using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
#pragma warning disable 0649

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    private EnemyAiStates state;
    
    
    public enum EnemyAiStates {Null,Idle, Attacking, Chasing, LowHealth, ReturnToSpawn, Dead, Hit, UniqueState, UniqueState2, UniqueState3, UniqueState4, StatusEffect };
    internal StatusEffects status = new StatusEffects();
    [SerializeField]
    internal StatsController stats = new StatsController();
    #region Enemy Health Bar
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject lockOnArrow;
    #endregion
    #region Special Effects
    [SerializeField] private GameObject deathEffect;

    #endregion
    [SerializeField] private int level;
    [SerializeField] private int attackDelay;
    [SerializeField] private int baseExpYield;
    [SerializeField] private int baseHealth;
    [SerializeField] private GameObject hitBox;
    
    [SerializeField] private GameObject drop;
    [SerializeField] private Slider EnemyHp;

    #region Script References
    private NavMeshAgent nav;
    private Player pc;
    private PlayerBattleSceneMovement pb;
    private Animator anim;

    #endregion

    #region Coroutines
    private Coroutine hitCoroutine;
    private Coroutine attackCoroutine;
    private Coroutine attackingCoroutine;
    private Coroutine recoveryCoroutine;
    private Coroutine guardCoroutine;
    #endregion
    private byte eaten;
    

    private Vector3 startLocation;
    [SerializeField] private GameObject slimeTree;
    [SerializeField] private GameObject slime;

    
    
    private bool attacking;
    private bool attack;
    private bool walk;
    private bool hit;
    private bool lockedOn;
    private bool dead;
    private bool lowHealth;
    [SerializeField] private int flip;
    private static List<Enemy> enemies = new List<Enemy>(32);
    private int behavior;
    private bool grounded;

    [SerializeField]private bool boss;

    public static event UnityAction<Enemy> onAnyDefeated;
    public static event UnityAction onAnyEnemyDead;

    #region Getters and Setters
public int Health { get { return stats.Health; } set { stats.Health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return stats.HealthLeft; } set { stats.HealthLeft = Mathf.Max(0, value); UIMaintence(); if (stats.HealthLeft <= 0 && !Dead) { Dead = true; } } }

    public bool Attack { get => attack; set { attack = value; Anim.SetBool("Attack", attack); } }
    public bool Walk { get => walk; set { walk = value; Anim.SetBool("Walking", walk); } }

    public bool Hit { get => hit; set { hit = value; if (Hit) { recoveryCoroutine = StartCoroutine(RecoveryCoroutine()); GetComponent<Rigidbody>().isKinematic = false; hitCoroutine = StartCoroutine(HitCoroutine()); } Anim.SetBool("Hurt", hit); } }
    public EnemyAiStates State { get => state; set { state = value; States(); } }
    public bool Grounded { get => grounded; set => grounded = value; }
    public bool LockedOn
    {
        get => lockedOn; set
        {
            lockedOn = value; if (lockedOn)
            {
                
                canvas.SetActive(true);
                lockOnArrow.SetActive(true);
            }
            else
            {
                lockOnArrow.SetActive(false);
                canvas.SetActive(false);
            }

        }
    }
    public bool Dead
    {
        get => dead;
        private set
        {
            dead = value;
            if (dead)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_onOrOff", 1);
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_452897A1", 1);
                
                OnDefeat();
                Anim.SetBool("Dead", dead);
                if (onAnyDefeated != null)
                {
                    onAnyDefeated(this);
                }
                if (onAnyEnemyDead != null)
                {
                    onAnyEnemyDead();
                }

            }
        }
    }

    #endregion
    
    public static int TotalCount => Enemies.Count;

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        StatusEffects.onStatusUpdate += StatusControl;
        StatCalculation();
        state = EnemyAiStates.Null;
    }
    // Start is called before the first frame update
    public void OnEnable()
    {
        EnemyHitBoxBehavior[] behaviours = Anim.GetBehaviours<EnemyHitBoxBehavior>();
        for (int i = 0; i < behaviours.Length; i++)
            behaviours[i].HitBox = hitBox;
    }
    public virtual void Start()
    {

        pc = Player.GetPlayer();
        GameController.onQuitGame += OnPlayerDeath;
        Player.onPlayerDeath += OnPlayerDeath;
        onAnyDefeated += EnemyDeath;
        Enemies.Add(this);
        pb = pc.GetComponent<PlayerBattleSceneMovement>();
        //InvokeRepeating("Attacking", 2f, 2f);
        level += Player.GetPlayer().stats.Level;
        //Health = level * baseHealth;
        startLocation = transform.position;
        HealthLeft = stats.Health;
        attackingCoroutine = StartCoroutine(AttackingCoroutine());
        StartCoroutine(WaitToState());
    }



    // Update is called once per frame
    public virtual void Update()
    {
        if (status.Status != StatusEffects.Statuses.stunned&&state!=EnemyAiStates.Null)
        {
            StateSwitch();

        }

        canvas.transform.rotation = Quaternion.LookRotation(transform.position - CameraLogic.PrespCam.transform.position);
    }
    private IEnumerator WaitToState() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        state = EnemyAiStates.Idle;
    }
    private void StatusControl()
    {
        if (!dead)
        {
            switch (status.Status)
            {
                case StatusEffects.Statuses.stunned:
                    State = EnemyAiStates.StatusEffect;
                    StartCoroutine(StatusCoroutine(3));
                    if (!dead) { Anim.speed = 0; }

                    break;


            }
        }
    }
    private void StatCalculation()
    {

        Health = stats.BaseHealth * level;
        stats.Attack = stats.BaseAttack * (level / 2);
        stats.Defense = stats.BaseDefense * level;


    }
    private IEnumerator StatusCoroutine(float StatusLength)
    {
        YieldInstruction wait = new WaitForSeconds(StatusLength);
        yield return wait;
        State = EnemyAiStates.Idle;
        Anim.speed = 1;
        status.Status = StatusEffects.Statuses.neutral;

    }
    public static Enemy GetEnemy(int i) => Enemies[i];
    public void OnPlayerDeath()
    {
        Enemies.Clear();
    }
    private void EnemyDeath(Enemy enemy)
    {


    }
    void StateSwitch()
    {

        if (state != EnemyAiStates.LowHealth)
        {
            if (State != EnemyAiStates.Chasing && nav.enabled && !dead)
            {
                Walk = false;
                nav.SetDestination(transform.position);
            }
            if (state != EnemyAiStates.Attacking)
            {
                attacking = false;

            }
            if (Distance < 1f && !dead && !hit)
            {

                State = EnemyAiStates.Attacking;

            }

            if (Distance > 1.1f && Distance < 6f && !dead&&!hit)
            {
                nav.enabled = true;
                //Debug.Log("fuk");
                State = EnemyAiStates.Chasing;
            }

            if (Hit) { State = EnemyAiStates.Hit; }
            if (Dead) { State = EnemyAiStates.Dead; }
        }


    }
    void States()
    {
        switch (state)
        {
            case EnemyAiStates.Idle:
                Idle();
                break;
            case EnemyAiStates.Attacking:
                attacking = true;
                break;
            case EnemyAiStates.LowHealth:
                LowHealth();
                break;
            case EnemyAiStates.ReturnToSpawn:
                ReturnToSpawn();
                break;
            case EnemyAiStates.Chasing:
                Chasing();
                break;

        }
    }
    //public  void FixedUpdate() {  }
    public virtual void Attacking()
    {

        Attack = true;
        attackCoroutine = StartCoroutine(AttackCoroutine());
        //hitBox.SetActive(true);
    }
    public virtual void Idle()
    {
        Walk = false;
    }
    private void LowHealth()
    {
        switch (behavior)
        {
            case 1:
                Flee();
                break;

            case 4:
                GetHelp();
                break;
        }
    }
    public virtual void Flee()
    {
        int rand = Random.Range(1, Enemies.Count - 1);
        Enemy target = GetEnemy(rand);
        if (target != null && !Dead && nav.enabled)
        {
            nav.SetDestination(target.transform.position);
            if (Vector3.Distance(target.transform.position, transform.position) < 1f)
            {
                Canniblize(target);
            }
        }
        else
            State = EnemyAiStates.Idle;
    }
    public virtual void PlantATree()
    {
        Instantiate(slimeTree, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
        slimeTree.transform.position = transform.position;
        State = EnemyAiStates.Idle;

    }
    public virtual void SpawnAFriend()
    {
        Instantiate(slime, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
        slime.transform.position = transform.position;
        State = EnemyAiStates.Idle;
    }
    private void GetHelp()
    {

    }
    public virtual void Canniblize(Enemy target)
    {
        //int rand = Random.Range(1,enemies.Count);
        level += Mathf.Min(1, (int)(0.50f * (target.level))); ;
        HealthLeft += Health;
        target.OnDefeat();
        //eaten++;
        if (eaten >= 5)
        {
            State = EnemyAiStates.UniqueState2;
        }
    }
    private void SlimeGolem()
    {

    }
    private void ReturnToSpawn()
    {
        nav.SetDestination(startLocation);
        Walk = true;
        if (Vector3.Distance(startLocation, transform.position) < 1f)
        {
            State = EnemyAiStates.Idle;
        }
    }
    public virtual void Chasing()
    {
        Walk = true;
        //transform.position = Vector3.MoveTowards(transform.position, Player.GetPlayer().transform.position, 1 * Time.deltaTime);
        if (Distance > 6 && !dead)
        {
            State = EnemyAiStates.ReturnToSpawn;
        }
        nav.SetDestination(Player.GetPlayer().transform.position);
    }
    private void UIMaintence()
    {

        levelText.GetComponent<Text>().text = "Lv. " + level;
        EnemyHp.maxValue = stats.Health;
        EnemyHp.value = stats.HealthLeft;
    }
    private void OnHit()
    {
        GameObject d = new GameObject();
        d.transform.SetParent(canvas.transform);
        d.transform.localPosition = new Vector3(0.4F, 0, 0);
        d.transform.rotation = new Quaternion(0, 0, 0, 0);
        d.AddComponent<Text>();
        d.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        d.GetComponent<Text>().resizeTextForBestFit = true;
        d.GetComponent<Text>().color = Color.red;
        d.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        d.GetComponent<Text>().text = "- " + Mathf.Abs(level - (2 * pc.stats.Attack)).ToString();
        Destroy(d, 2f);

    }
    private IEnumerator HitCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(4);
        yield return wait;
        Hit = false;
        State = EnemyAiStates.Idle;

    }
    private IEnumerator RecoveryCoroutine()
    {
        while (!nav.enabled)
        {
            YieldInstruction wait = new WaitForSeconds(3);
            yield return wait;
            if (Grounded)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                nav.enabled = true;
            }
        }


    }
    private IEnumerator AttackCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        transform.rotation = Quaternion.LookRotation((flip) * (transform.position - pc.transform.position));
        Attack = false;
        //hitBox.SetActive(false);

    }
    private IEnumerator AttackingCoroutine()
    {

        while (isActiveAndEnabled)
        {
            YieldInstruction wait = new WaitForSeconds(attackDelay);
            yield return wait;
            if (attacking)
            {
                Attacking();

            }
        }
    }
    private IEnumerator StateControlCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(3.5f);
        yield return wait;
        State = EnemyAiStates.LowHealth;
        LowHealth();
        behavior = Random.Range(1, 4);
        StateControl();
        lowHealth = true;
    }
    private void StateControl()
    {
        if (!dead)
        {
            switch (behavior)
            {
                case 2:
                    PlantATree();
                    break;
                case 3:
                    SpawnAFriend();
                    break;
            }
        }
    }
    private float Distance => Vector3.Distance(pc.transform.position, transform.position);

    public bool Boss { get => boss; set => boss = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public static List<Enemy> Enemies { get => enemies; set => enemies = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && !other.CompareTag("Enemy") && other.CompareTag("Attack"))
        {
            Grounded = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Boss) { 
        if (other.gameObject.CompareTag("Attack")) {
            GetComponent<NavMeshAgent>().enabled = false;

        }}
    }
    public void KnockBack(Vector3 hitForce) {
        if (!boss) { 
        GetComponent<Rigidbody>().AddForce(hitForce, ForceMode.VelocityChange);}

    }
    public void OnDefeat()
    {
        //onAnyDefeated(this);
        SlimeHasDied();
        Enemies.Remove(this);
        Instantiate(deathEffect, transform);
        //deathEffect.transform.position = transform.position;
        Destroy(gameObject, 2.5f);
        //drop.transform.SetParent(null);
    }
    public void CalculateDamage(float addition)
    {
        if (!dead) { 
        HealthLeft -= Mathf.Max(1, (pc.stats.Attack+(int)addition) - stats.Defense);//WRITE THE FUCKING ENEMY'S STATS CLASS
        Hit = true;
        if (HealthLeft <= Health / 4 && !lowHealth)
        {

            StartCoroutine(StateControlCoroutine());
            lowHealth = true;
        }
        OnHit();}
    }//(Mathf.Max(1, (int)(Mathf.Pow(stats.Attack - 2.6f * pc.stats.Defense, 1.4f) / 30 + 3))) / n; }
    public void CalculateAttack() { pc.stats.HealthLeft -= Mathf.Max(1, stats.Attack - (int)(stats.Defense * 1.6f)); }
    public void SlimeHasDied()
    {
        int exp = baseHealth * baseExpYield;
        pc.stats.AddExp(exp);
        Instantiate(drop, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
        drop.transform.position = transform.position;

    }

}
