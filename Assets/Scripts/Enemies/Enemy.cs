using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
#pragma warning disable 0649


public class Enemy : MonoBehaviour
{

    private EnemyAiStates state;
    public enum EnemyType { Slime, Samurai };
    [SerializeField] private EnemyType type;
    public enum EnemyAiStates { Idle, Attacking, Chasing, LowHealth, ReturnToSpawn, Dead, Hit, Canniblize, Transform, GetHelp, PlantSlimeTree };
    [SerializeField] private int level;
    [SerializeField] private int attackDelay;
    [SerializeField] private int baseExpYield;
    [SerializeField] private int baseHealth;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject drop;
    [SerializeField] private Slider EnemyHp;
    [SerializeField] private GameObject deathEffect;
    private byte eaten;
    private Player pc;
    private PlayerBattleSceneMovement pb;
    private Animator anim;

    private Vector3 startLocation;
    [SerializeField] private GameObject slimeTree;
    [SerializeField] private GameObject slime;
    private int health;
    private int healthLeft;
    private NavMeshAgent nav;
    private Coroutine hitCoroutine;
    private Coroutine attackCoroutine;
    private Coroutine attackingCoroutine;
    private Coroutine recoveryCoroutine;
    private Coroutine guardCoroutine;
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

    
    public static event UnityAction<Enemy> onAnyDefeated;
    public static event UnityAction onAnyEnemyDead;

    public int Health { get { return health; } set { health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return healthLeft; } set { healthLeft = Mathf.Max(0, value); UIMaintence(); if (healthLeft <= 0 && !Dead) { Dead = true; } } }
    
    public bool Attack { get => attack; set { attack = value; anim.SetBool("Attack", attack); } }
    public bool Walk { get => walk; set { walk = value; anim.SetBool("Walking", walk); } }

    public bool Hit { get => hit; set { hit = value; if (Hit) { recoveryCoroutine = StartCoroutine(RecoveryCoroutine()); GetComponent<Rigidbody>().isKinematic = false; hitCoroutine = StartCoroutine(HitCoroutine()); } anim.SetBool("Hurt", hit); } }

    public bool LockedOn
    {
        get => lockedOn; set
        {
            lockedOn = value; if (lockedOn)
            {
                canvas.SetActive(true);
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
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_452897A1", 1);

                OnDefeat();
                anim.SetBool("Dead", dead);
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
    public static int TotalCount => enemies.Count;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

    }
    // Start is called before the first frame update
    public void OnEnable()
    {
        EnemyHitBoxBehavior[] behaviours = anim.GetBehaviours<EnemyHitBoxBehavior>();
        for (int i = 0; i < behaviours.Length; i++)
            behaviours[i].HitBox=hitBox;
    }
    public virtual void Start()
    {
        
        pc = Player.GetPlayer();
        GameController.onQuitGame += OnPlayerDeath;
        Player.onPlayerDeath += OnPlayerDeath;
        onAnyDefeated += EnemyDeath;
        enemies.Add(this);
        pb = pc.GetComponent<PlayerBattleSceneMovement>();
        //InvokeRepeating("Attacking", 2f, 2f);
        level += Player.GetPlayer().stats.Level;
        Health = level * baseHealth;
        startLocation = transform.position;
        HealthLeft = health;
        attackingCoroutine = StartCoroutine(AttackingCoroutine());
    }
    public EnemyAiStates State { get => state; set => state = value; }


    // Update is called once per frame
    public virtual void Update()
    {

        StateSwitch();
        canvas.transform.rotation = Quaternion.LookRotation(transform.position - CameraLogic.PrespCam.transform.position);
    }
    public static Enemy GetEnemy(int i) { return enemies[i]; }
    public void OnPlayerDeath()
    {
        enemies.Clear();
    }
    private void EnemyDeath(Enemy enemy)
    {


    }
    void StateSwitch()
    {

        if (State != EnemyAiStates.LowHealth)
        {
            if (State != EnemyAiStates.Chasing && nav.enabled && !dead)
            {
                Walk = false;
                nav.SetDestination(transform.position);
            }
            if (State != EnemyAiStates.Attacking)
            {
                attacking = false;

            }
            if (Distance < 1f && !dead && !hit)
            {

                State = EnemyAiStates.Attacking;

            }

            if (Distance > 1.1f && Distance < 6f && !dead && nav.enabled)
            {
                //Debug.Log("fuk");
                State = EnemyAiStates.Chasing;
            }

            if (Hit) { State = EnemyAiStates.Hit; }
            if (Dead) { State = EnemyAiStates.Dead; }
        }

        States();
    }
    void States()
    {
        switch (State)
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
    //public virtual void FixedUpdate() {  }
    private void Attacking()
    {

        Attack = true;
        attackCoroutine = StartCoroutine(AttackCoroutine());
        //hitBox.SetActive(true);
    }
    private void Idle()
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
    private void Flee()
    {
        int rand = Random.Range(1, enemies.Count - 1);
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
    private void PlantATree()
    {
        Instantiate(slimeTree, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
        slimeTree.transform.position = transform.position;
        State = EnemyAiStates.Idle;

    }
    private void SpawnAFriend()
    {
        Instantiate(slime, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
        slime.transform.position = transform.position;
        State = EnemyAiStates.Idle;
    }
    private void GetHelp()
    {

    }
    private void Canniblize(Enemy target)
    {
        //int rand = Random.Range(1,enemies.Count);
        level += Mathf.Min(1, (int)(0.10f * (target.level))); ;
        HealthLeft += Health;
        target.OnDefeat();
        //eaten++;
        if (eaten >= 5)
        {
            State = EnemyAiStates.Transform;
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
    private void Chasing()
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
        EnemyHp.maxValue = health;
        EnemyHp.value = healthLeft;
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
        YieldInstruction wait=new WaitForSeconds(4);
        yield return wait;
        Hit = false;

    }
    private IEnumerator RecoveryCoroutine()
    {

        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        GetComponent<Rigidbody>().isKinematic = true;
        nav.enabled = true;

    }
    private IEnumerator AttackCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        transform.rotation = Quaternion.LookRotation( (flip)*(transform.position- pc.transform.position));
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

    

    public void OnDefeat()
    {
        //onAnyDefeated(this);
        SlimeHasDied();
        enemies.Remove(this);
        Instantiate(deathEffect,transform);
        Destroy(gameObject, 4f);
        //drop.transform.SetParent(null);
    }
    public void CalculateDamage()
    {
        HealthLeft -= (int)(1.7f * pc.stats.Attack);//WRITE THE FUCKING ENEMY'S STATS CLASS
        Hit = true;
        if (HealthLeft <= Health / 4 && !lowHealth)
        {

            StartCoroutine(StateControlCoroutine());
            lowHealth = true;
        }
        OnHit();
    }
    public void CalculateAttack(int n) { pc.stats.HealthLeft -= (Mathf.Max(1, (int)(Mathf.Pow(8 * level - 2.6f * pc.stats.Defense, 1.4f) / 30 + 3))) / n; }
    public void SlimeHasDied()
    {
        int exp = level * baseExpYield;
        pc.stats.AddExp(exp);
        Instantiate(drop, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
        drop.transform.position = transform.position;

    }

}
