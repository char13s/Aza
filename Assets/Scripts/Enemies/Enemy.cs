using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using XInputDotNetPure;
#pragma warning disable 0649


[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

    private EnemyAiStates state;


    public enum EnemyAiStates { Null, Idle, Attacking, Chasing, LowHealth, ReturnToSpawn, Dead, Hit, UniqueState, UniqueState2, UniqueState3, UniqueState4, StatusEffect };
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
    [Space]
    [Header("Enemy Parameters")]
    [SerializeField] private int level;
    [SerializeField] private int attackDelay;
    [SerializeField] private int baseExpYield;
    [SerializeField] private int baseHealth;
    [SerializeField] private float attackDistance;
    [Space]
    [Header("Object Refs")]
    [SerializeField] private GameObject hitBox;
    
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject soul;
	[SerializeField] private GameObject cut;
    [SerializeField] private Slider EnemyHp;


    #region Script References
    
    private Player pc;
    private PlayerBattleSceneMovement pb;
    [SerializeField] private Animator anim;
    private EnemyTimelines timelines;
    //private AudioSource sound;
    private Rigidbody rbody;
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
    [SerializeField] private GameObject farHitPoint;
    [SerializeField] private GameObject slimeTree;
    [SerializeField] private GameObject slime;

    [SerializeField]private GameObject ouch;
    
    
    private bool attacking;
    private bool attack;
    private bool walk;
    private bool hit;
    private bool lockedOn;
    private bool dead;
    private bool lowHealth;
    [SerializeField]private bool weak;

    private bool striking;
    [SerializeField] private int flip;
    private static List<Enemy> enemies = new List<Enemy>(32);
    private int behavior;
    private bool grounded;

    [SerializeField]private bool boss;
	private bool frozen;
    private bool yeet;

    public static event UnityAction<Enemy> onAnyDefeated;
    public static event UnityAction onAnyEnemyDead;
	public static event UnityAction onHit;
    public static event UnityAction guardBreak;
    public static event UnityAction<AudioClip> sendsfx;
    #region Getters and Setters
public int Health { get { return stats.Health; } set { stats.Health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return stats.HealthLeft; } set { stats.HealthLeft = Mathf.Max(0, value); UIMaintence(); if (stats.HealthLeft <= 0 && !dead) { Dead = true; } } }

    public bool Attack { get => attack; set { attack = value; Anim.SetBool("Attack", attack); } }
    public bool Walk { get => walk; set { walk = value; Anim.SetBool("Walking", walk); } }

    public bool Hit { get => hit; set { hit = value; if (hit) { OnHit(); } Anim.SetBool("Hurt", hit); if (onHit != null) {
				onHit();
			}
		} }
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
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("dead", 1);
                
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
	public bool Boss { get => boss; set => boss = value; }
	public Animator Anim { get => anim; set => anim = value; }
	public static List<Enemy> Enemies { get => enemies; set => enemies = value; }
	public bool Frozen { get => frozen; set { frozen = value; if (frozen) { FreezeEnemy(); } } }

	#endregion

	public static int TotalCount => Enemies.Count;

    public virtual void Awake()
    {
        //Anim = GetComponent<Animator>();
        
        //sound = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody>();
        StatusEffects.onStatusUpdate += StatusControl;
        StatCalculation();
        state = EnemyAiStates.Null;
		ZaWarudo.timeFreeze += FreezeEnemy;
        UiManager.nullEnemies += FreezeEnemy;
        ShieldHitBox.punch += Punch;
        //UiManager.portal += EnemiesNeedToRespawn;
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
        PortalConnector.backToLevelSelect += OnPlayerDeath;
        //onAnyDefeated += EnemyDeath;
        ReactionRange.dodged += SlowEnemy;
        HitBox.sendFlying += KnockBack;
        //UiManager.killAll += KillEnemy;
        //EnemyHitBox.hit += CalculateAttack;
        //EnemyHitBox.guardHit += HitGuard;
        Enemies.Add(this);
        pb = pc.GetComponent<PlayerBattleSceneMovement>();
        timelines = GetComponent<EnemyTimelines>();
        //InvokeRepeating("Attacking", 2f, 2f);
       // level += Player.GetPlayer().stats.Level;
        //Health = level * baseHealth;
        startLocation = transform.position;
        HealthLeft = stats.Health;
        attackingCoroutine = StartCoroutine(AttackingCoroutine());
        StartCoroutine(WaitToState());
    }


    private void EnemiesNeedToRespawn(int c) {
        Destroy(gameObject);
    }
    // Update is called once per frame
    public virtual void Update()
    {
		if (status.Status != StatusEffects.Statuses.stunned&&state!=EnemyAiStates.Null)
        {
            //StateSwitch();

        }
        //canvas.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
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
        Health = stats.BaseHealth;
        stats.Attack = stats.BaseAttack;
        stats.Defense = stats.BaseDefense;
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
    public void Knocked() {
        Vector3 delta = (flip) * (transform.position - pc.transform.position);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        timelines.KnockedBack();
    }
    public void CancelKnocked() {
        timelines.CancelKnockUp();
        rbody.AddForce(new Vector3(0,-150,0),ForceMode.VelocityChange);
    }
    public void KnockedUp() {
        print("Knocked up");
        timelines.KnockUp();
    }
    private void KillEnemy() {
        Destroy(this);
    }
    #region Event handlers
    private void SwitchFreezeOn() {
		Frozen = true;
	}
	private void FreezeEnemy() {
        Debug.Log("Froze");
		anim.speed = 0;
		State = EnemyAiStates.Null;
		StartCoroutine(UnFreeze());
	}
	private IEnumerator UnFreeze() {
		YieldInstruction wait = new WaitForSeconds(4);
		yield return wait;
		UnFreezeEnemy();
	}
	private void UnFreezeEnemy() {
		anim.speed = 1;
		State = EnemyAiStates.Idle;
	}
    private void NullEnemy() {
        State = EnemyAiStates.Null;
    }
    #endregion
    
    private void StateSwitch()
    {
        switch (state) {
            case EnemyAiStates.Idle:
                break;
            case EnemyAiStates.Chasing:
                break;

        }
        if (state != EnemyAiStates.LowHealth)
        {
            if (state != EnemyAiStates.Chasing && !dead)
            {
                Walk = false;
                
                //nav.SetDestination(transform.position);
            }
            if (state != EnemyAiStates.Attacking)
            {
                attacking = false;

            }
            if (Distance < 1.5f && !dead && !Hit)
            {

                State = EnemyAiStates.Attacking;

            }

            if (Distance > 1.5f && Distance < 6f && !dead&&!Hit)
            {
                
                //Debug.Log("fuk");
                State = EnemyAiStates.Chasing;
            }
            if (Distance > 6 && !dead) {
                State = EnemyAiStates.ReturnToSpawn;
            }
            if (Hit) { State = EnemyAiStates.Hit;
            }
            if (Dead) { State = EnemyAiStates.Dead; }
        }


    }
    private void States()
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
            default:
                break;
        }
    }
    //public  void FixedUpdate() {  }
    public virtual void Attacking()
    {
        Attack = true;
        striking = true;
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
        if (target != null && !Dead)
        {
            
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
        if (slimeTree != null) { 
        Instantiate(slimeTree, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
        slimeTree.transform.position = transform.position;}
        State = EnemyAiStates.Idle;

    }
    public virtual void SpawnAFriend()
    {
        if (slime != null) { 
        Instantiate(slime, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
        slime.transform.position = transform.position;}
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
        
        Vector3 delta = (flip) * (transform.position - startLocation);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        Walk = true;
        transform.position = Vector3.MoveTowards(transform.position, startLocation, 4 * Time.deltaTime);
        if (Vector3.Distance(startLocation, transform.position) < 1f)
        {
            State = EnemyAiStates.Idle;
        }
    }
    public virtual void Chasing()
    {
        Walk = true;
        //transform.position = Vector3.MoveTowards(transform.position, Player.GetPlayer().transform.position, 1 * Time.deltaTime);
        //if (pc.Nav.enabled) {
        //    //nav.SetDestination(Player.GetPlayer().transform.position);
        //    
        //}
        Vector3 delta = (flip) * (transform.position - pc.transform.position);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        //transform.rotation = Quaternion.LookRotation((flip) * (transform.position - pc.transform.position));
        transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, 4 * Time.deltaTime);
        
    }
    private void UIMaintence()
    {

        levelText.GetComponent<Text>().text = "Lv. " + level;
        EnemyHp.maxValue = stats.Health;
        EnemyHp.value = stats.HealthLeft;
    }

    private void OnHit()
    {
        //sound.PlayOneShot(AudioManager.GetAudio().SlimeHit);
        if (sendsfx != null) {
            sendsfx(AudioManager.GetAudio().SlimeHit);
        }
        if (state != EnemyAiStates.Null) {
            hitCoroutine = StartCoroutine(HitCoroutine());

        }
        //transform.rotation = Quaternion.LookRotation((flip) * (transform.position - pc.transform.position));
        Instantiate(cut,transform);
        //StopCoroutine(attackCoroutine);
    }
    
    private IEnumerator HitCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(3.5f);
        yield return wait;
        Hit = false;
        State = EnemyAiStates.Idle;
    }
 
    private IEnumerator AttackCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        transform.rotation = Quaternion.LookRotation((flip) * (transform.position - pc.transform.position));
        Attack = false;
        striking = false;
        //hitBox.SetActive(false);

    }
    private IEnumerator AttackingCoroutine()
    {
        while (isActiveAndEnabled)
        {
            YieldInstruction wait = new WaitForSeconds(AttackDelay);
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

    public GameObject FarHitPoint { get => farHitPoint; set => farHitPoint = value; }
    public bool Yeet { get => yeet; set { StartCoroutine(ResetYeet()); yeet = value; } }

    public int AttackDelay { get => attackDelay; set => attackDelay = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && !other.CompareTag("Enemy") && other.CompareTag("Attack"))
        {
            Grounded = true;
        }
    }
    
    private void KnockBack(Enemy en,float hitForce) {
        if (en==this) { 
        rbody.AddForce(transform.forward*-hitForce*2,ForceMode.Impulse);
        Debug.Log("push");}
    }
    private void Punch(Enemy enemy) {
        if (this == enemy) {
            Hit = true;
            Debug.Log("Works");
            StartCoroutine(Punched());
            StartCoroutine(StopKnockBack());
            Yeet = false;
        }       
    }
    private IEnumerator Punched() {
        while (isActiveAndEnabled&&!yeet) {
            Debug.Log("yeet");
        yield return null;
       transform.position=Vector3.MoveTowards(transform.position, farHitPoint.transform.position, 50*Time.deltaTime);
        }
        //rbody.AddForce(Vector3.forward * -50, ForceMode.Impulse);
    }
    private IEnumerator StopKnockBack() {
        YieldInstruction wait = new WaitForSeconds(0.09f);
        yield return wait;
        Yeet = true;
        StopCoroutine(Punched());
        Debug.Log("stopped");
    }
    private IEnumerator ResetYeet() {
        yield return null;
        Yeet = false;
    }
    private void SlowEnemy() {
        if (striking) {
            FreezeEnemy();
        }
    }
    public void OnDefeat()
    {
        //onAnyDefeated(this);
        SlimeHasDied();
        Enemies.Remove(this);
        Instantiate(deathEffect, transform);
        //deathEffect.transform.position = transform.position;
        Destroy(gameObject, 2.5f);
        switch (pc.Weapon) {
            case 0:
                pc.stats.SwordProficency += 5;
                break;
            case 1:
                break;
        }
        //drop.transform.SetParent(null);
    }
    public void CalculateDamage(float addition)
    {
        if (!dead) {
            HealthLeft -= Mathf.Clamp((pc.stats.Attack - stats.Defense),0,999);  
            Hit = true;
        if (HealthLeft <= Health / 4 && !lowHealth)
        {

            StartCoroutine(StateControlCoroutine());
            lowHealth = true;
        }
        OnHit();}
        
    }//(Mathf.Max(1, (int)(Mathf.Pow(stats.Attack - 2.6f * pc.stats.Defense, 1.4f) / 30 + 3))) / n; }
    public void CalculateAttack() {
        if (!weak) {
            pc.stats.HealthLeft -= Mathf.Max(1, stats.Attack);
        }
    }
    public void HitGuard() {
        if (pc.stats.MPLeft > 0) {
            pc.stats.MPLeft -= Mathf.Max(1, stats.Attack );
            if (sendsfx != null) {
                sendsfx(AudioManager.GetAudio().HitShield);
            }
        }
        else {
            
            if (guardBreak != null) {
                guardBreak();
            }
        } 
    }
    public void SlimeHasDied()
    {
        int exp = baseHealth * baseExpYield;
        pc.stats.AddExp(exp);
        if (drop != null&&!weak) { 
        Instantiate(drop, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
        drop.transform.position = transform.position;
            
        }
        if (soul != null) {
            Instantiate(soul, transform.position + new Vector3(0, 0.18f, 0), Quaternion.identity);
            soul.transform.position = transform.position;
        }

    }

}
