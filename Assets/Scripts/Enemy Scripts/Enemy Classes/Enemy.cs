using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using XInputDotNetPure;
#pragma warning disable 0649

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyTimelines))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    private EnemyAiStates state;


    public enum EnemyAiStates { Null, Idle, Attacking, Chasing, LowHealth, ReturnToSpawn, Dead, Hit, UniqueState, UniqueState2, UniqueState3, UniqueState4, StatusEffect };
    internal StatusEffects status = new StatusEffects();
    [SerializeField]
    internal StatsController stats = new StatsController();
    [Header("Enemy Health Bar")]
    #region Enemy Health Bar
    [SerializeField] private GameObject canvas;
    //[SerializeField] private Text levelText;
    [SerializeField] private GameObject lockOnArrow;
    #endregion
    #region Special Effects
    [SerializeField] private GameObject deathEffect;

    #endregion
    [Space]
    [Header("Enemy Parameters")]
    //[SerializeField] private int level;
    [SerializeField] private int attackDelay;
    //[SerializeField] private int baseExpYield;
    //[SerializeField] private int baseHealth;
    [SerializeField] private float attackDistance;
    [SerializeField] private bool standby;
    [Space]
    [Header("Object Refs")]
    [SerializeField] protected GameObject hitBox;
    [SerializeField] private GameObject hitSplat;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject soul;
    [SerializeField] private GameObject cut;
    [SerializeField] private Slider EnemyHp;
    [SerializeField] private float speed;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject finisherTrigger;
    #region Script References

    private Player pc;
    private Animator anim;
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
    //private byte eaten;
    private bool attacking;
    private bool attack;
    private bool walk;
    private bool hit;
    private bool lockedOn;
    private bool dead;
    private bool lowHealth;
    
    // [SerializeField] private bool weak;

    private bool striking;
    [SerializeField] private int flip;
    private static List<Enemy> enemies = new List<Enemy>(32);
    private bool grounded;

    [SerializeField] private bool boss;
    private bool frozen;

    public static event UnityAction<Enemy> onAnyDefeated;
    public static event UnityAction onAnyEnemyDead;
    public static event UnityAction onHit;
    public static event UnityAction guardBreak;
    public static event UnityAction<AudioClip> sendsfx;
    #region Getters and Setters
    public int Health { get { return stats.Health; } set { stats.Health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return stats.HealthLeft; } set { stats.HealthLeft = Mathf.Max(0, value); UIMaintence(); canvas.GetComponent<EnemyCanvas>().SetEnemyHealth(); if (stats.HealthLeft <= 0 && !dead) { Dead = true; } } }

    public bool Attack { get => attack; set { attack = value; Anim.SetBool("Attack", attack); } }
    public bool Walk { get => walk; set { walk = value; Anim.SetBool("Walking", walk); } }

    public bool Hit {
        get => hit; set {
            hit = value;
            Anim.SetBool("Hurt", hit); if (onHit != null) {
                onHit();
            }
            if (hit) { OnHit(); }
        }
    }
    public EnemyAiStates State { get => state; set { state = value; States(); } }
    public bool Grounded { get => grounded; set { grounded = value; Anim.SetBool("Grounded",grounded); } }
    public bool LockedOn {
        get => lockedOn; set {
            lockedOn = value; if (lockedOn) {

                canvas.SetActive(true);
                lockOnArrow.SetActive(true);
            }
            else {
                lockOnArrow.SetActive(false);
                canvas.SetActive(false);
            }

        }
    }
    public bool Dead {
        get => dead;
        private set {
            dead = value;
            if (dead) {
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_onOrOff", 1);
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("dead", 1);

                OnDefeat();
                Anim.SetBool("Dead", dead);
                if (onAnyDefeated != null) {
                    onAnyDefeated(this);
                }
                if (onAnyEnemyDead != null) {
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

    public virtual void Awake() {
        Anim = model.GetComponent<Animator>();
        //sound = GetComponent<AudioSource>();
        Rbody = GetComponent<Rigidbody>();
        //StatusEffects.onStatusUpdate += StatusControl;
        StatCalculation();
        state = EnemyAiStates.Idle;
        ZaWarudo.timeFreeze += FreezeEnemy;
        UiManager.nullEnemies += FreezeEnemy;
    }
    // Start is called before the first frame update
    public void OnEnable() {

    }
    public virtual void Start() {
        #region Grabbing Behaviors here
        EnemyHitBoxBehavior[] hitBoxBehaviors = Anim.GetBehaviours<EnemyHitBoxBehavior>();
        for (int i = 0; i < hitBoxBehaviors.Length; i++)
            hitBoxBehaviors[i].HitBox = hitBox;

        EnemyHitBehavior[] hitBehaviors = Anim.GetBehaviours<EnemyHitBehavior>();
        for (int i = 0; i < hitBehaviors.Length; i++)
            hitBehaviors[i].Enemy = this;

        EnemyChaseBehavior[] chaseBehaviors = Anim.GetBehaviours<EnemyChaseBehavior>();
        for (int i = 0; i < chaseBehaviors.Length; i++)
            chaseBehaviors[i].Enemy = this;
        #endregion
        if (hitBoxBehaviors.Length>0) {
           print("Behavior check!");
            if (hitBoxBehaviors[0].HitBox != null) {
            print("Hit Box check!");
        }
        }
        
        pc = Player.GetPlayer();
        GameController.onQuitGame += OnPlayerDeath;
        Player.onPlayerDeath += OnPlayerDeath;
        PortalConnector.backToLevelSelect += OnPlayerDeath;
        ReactionRange.dodged += SlowEnemy;
        Enemies.Add(this);
        timelines = GetComponent<EnemyTimelines>();
        HealthLeft = stats.Health;
        StandbyState();
    }


    private void EnemiesNeedToRespawn(int c) {
        Destroy(gameObject);
    }
    // Update is called once per frame
    public virtual void Update() {
        Distance = Vector3.Distance(pc.transform.position, transform.position);
        if (status.Status != StatusEffects.Statuses.stunned && state != EnemyAiStates.Null) {
            StateSwitch();

        }
        //canvas.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
    /*private IEnumerator WaitToState() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        state = EnemyAiStates.Idle;
    }*/

    private void StatusControl() {
        if (!dead) {
            switch (status.Status) {
                case StatusEffects.Statuses.stunned:
                    State = EnemyAiStates.StatusEffect;
                    if (!dead) { Anim.speed = 0; }

                    break;
            }
        }
    }
    private void StatCalculation() {
        Health = stats.BaseHealth * stats.Level;
        stats.Attack = stats.BaseAttack * stats.Level;
        stats.Defense = stats.BaseDefense * stats.Level;
    }
    /*private IEnumerator StatusCoroutine(float StatusLength) {
        YieldInstruction wait = new WaitForSeconds(StatusLength);
        yield return wait;
        State = EnemyAiStates.Idle;
        Anim.speed = 1;
        status.Status = StatusEffects.Statuses.neutral;

    }*/
    public static Enemy GetEnemy(int i) => Enemies[i];
    public void OnPlayerDeath() {
        Enemies.Clear();
    }
    #region Reactions
    public void Knocked() {
        Vector3 delta = (pc.transform.position-transform.position);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        timelines.KnockedBack();
    }
    public void CancelKnocked() {
        timelines.CancelKnockUp();
        Rbody.AddForce(new Vector3(0, -150, 0), ForceMode.VelocityChange);
    }
    public void KnockedUp() {
        print("Knocked up");
        timelines.KnockUp();
    }
    private void KillEnemy() {
        Destroy(this);
    }
    #region old freeze
    private void SwitchFreezeOn() {
        Frozen = true;
    }
    private void FreezeEnemy() {
        Debug.Log("Froze");
        Anim.SetFloat("Speed",0.1f);
        //anim.speed = 0;
        //State = EnemyAiStates.Null;
        StartCoroutine(UnFreeze());
    }
    private IEnumerator UnFreeze() {
        YieldInstruction wait = new WaitForSeconds(4);
        yield return wait;
        Anim.SetFloat("Speed", 0.1f);
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
#endregion
    #region Event handlers


    #endregion

    #region State Logic
    private void StateSwitch() {
        /*switch (state) {
            case EnemyAiStates.Idle:
                break;
            case EnemyAiStates.Chasing:
                break;

        }*/
        if (HealthLeft < Health / 4) {
            lowHealth = true;
            finisherTrigger.SetActive(true);
        }
        if (state == EnemyAiStates.Chasing) {
            SwitchToAttack();
            BackToIdle();
        }
        if (state == EnemyAiStates.Idle) {//What happens in Idle
            SwitchToAttack();
            ChasePlayer();
        }
        if (state == EnemyAiStates.Attacking) {
            ChasePlayer();
            BackToIdle();
        }
        /*if (state != EnemyAiStates.LowHealth) {
            if (state != EnemyAiStates.Chasing && !dead) {
                Walk = false;

                //nav.SetDestination(transform.position);
            }
            if (state != EnemyAiStates.Attacking) {
                attacking = false;

            }

            if (Distance > 6 && !dead) {
                State = EnemyAiStates.ReturnToSpawn;
            }
            if (Hit) {
                State = EnemyAiStates.Hit;
            }
            if (Dead) { State = EnemyAiStates.Dead; }
        }
        */

    }
    private void SwitchToAttack() {
        if (Distance < 1.5f && !dead && !Hit) {
            State = EnemyAiStates.Attacking;
        }
        else {
            attacking = false;
        }
    }
    private void ChasePlayer() {
        if (Distance > 1.5f && Distance < 6f && !dead && !Hit) {
            State = EnemyAiStates.Chasing;
        }
        else {
            Walk = false;
        }
    }
    private void BackToIdle() {
        if (Distance > 10f) {
            State = EnemyAiStates.Idle;
        }
    }

    private void States() {
        switch (state) {
            case EnemyAiStates.Idle:
                Idle();
                break;
            case EnemyAiStates.Attacking:
                Rbody.velocity = new Vector3(0, 0, 0);
                Anim.SetTrigger("Attack 0");
                break;
            case EnemyAiStates.LowHealth:
                //LowHealth();
                break;
            case EnemyAiStates.Chasing:
                Walk = true;
                //Chasing();
                break;
            default:
                break;
        }
    } public virtual void Idle() {
        Walk = false;
        Debug.Log("Idle asf");
    }
    //public  void FixedUpdate() {  }
    /*public virtual void Attacking() {
        Attack = true;
        striking = true;
        attackCoroutine = StartCoroutine(AttackCoroutine());
        //hitBox.SetActive(true);
    }
    
    private void LowHealth() {
        switch (behavior) {
            case 1:
                Flee();
                break;

            case 4:
                GetHelp();
                break;
        }
    }
    public virtual void Flee() {
        int rand = Random.Range(1, Enemies.Count - 1);
        Enemy target = GetEnemy(rand);
        if (target != null && !Dead) {

            if (Vector3.Distance(target.transform.position, transform.position) < 1f) {
                Canniblize(target);
            }
        }
        else
            State = EnemyAiStates.Idle;
    }
    public virtual void PlantATree() {
        if (slimeTree != null) {
            Instantiate(slimeTree, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
            slimeTree.transform.position = transform.position;
        }
        State = EnemyAiStates.Idle;

    }
    public virtual void SpawnAFriend() {
        if (slime != null) {
            Instantiate(slime, transform.position + new Vector3(4, 0.14f, 0), Quaternion.identity);
            slime.transform.position = transform.position;
        }
        State = EnemyAiStates.Idle;
    }
    private void GetHelp() {

    }
    public virtual void Canniblize(Enemy target) {
        //int rand = Random.Range(1,enemies.Count);
        stats.Level += Mathf.Min(1, (int)(0.50f * (target.stats.Level))); ;
        HealthLeft += Health;
        target.OnDefeat();
        //eaten++;
        if (eaten >= 5) {
            State = EnemyAiStates.UniqueState2;
        }
    }
    private void SlimeGolem() {

    }
    private void ReturnToSpawn() {

        Vector3 delta = (flip) * (transform.position - startLocation);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        Walk = true;
        //transform.position = Vector3.MoveTowards(transform.position, startLocation, 4 * Time.deltaTime);
        if (Vector3.Distance(startLocation, transform.position) < 1f) {
            State = EnemyAiStates.Idle;
        }
    }*/
    public void Chasing() {

        Walk = true;
        Vector3 delta = pc.transform.position - transform.position;
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        Rbody.velocity = transform.forward * speed;
        //Debug.Log(state + " a bitch");
        //Debug.Log("Chasing");
        //transform.rotation = Quaternion.LookRotation((flip) * (transform.position - pc.transform.position));
        //transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, 4 * Time.deltaTime);
        //Debug.Log(Vector3.MoveTowards(transform.position, pc.transform.position, 4 * Time.deltaTime));
    }
    private void StandbyState() {
        if (standby) {
            State = EnemyAiStates.Null;
        }
    }
    #endregion
    private void UIMaintence() {

        //levelText.GetComponent<Text>().text = "Lv. " + stats.Level;
        EnemyHp.maxValue = stats.Health;
        EnemyHp.value = stats.HealthLeft;
    }
    private void OnHit() {
        /* //sound.PlayOneShot(AudioManager.GetAudio().SlimeHit);
         if (sendsfx != null) {
             sendsfx(AudioManager.GetAudio().SlimeHit);
         }
         if (state != EnemyAiStates.Null) {
             hitCoroutine = StartCoroutine(HitCoroutine());
         }
         Instantiate(cut, transform);
        private IEnumerator HitCoroutine() {
         YieldInstruction wait = new WaitForSeconds(3.5f);
         yield return wait;
         Hit = false;
         State = EnemyAiStates.Idle;
     }*/
        rbody.useGravity = false;
        StartCoroutine(waitToFall());
    }


    #region Coroutines
    IEnumerator waitToFall() { 
        YieldInstruction wait =new WaitForSeconds(1);
        yield return wait;
        rbody.useGravity = true;
    }
    #endregion

    private float Distance ;

    public int AttackDelay { get => attackDelay; set => attackDelay = value; }
    public Rigidbody Rbody { get => rbody; set => rbody = value; }
    public bool Standby { get => standby; set { standby = value; StandbyState(); } } 

    //private void OnTriggerStay(Collider other) {
    //    if (other != null && !other.CompareTag("Enemy") && other.CompareTag("Attack")) {
    //        Grounded = true;
    //    }
    //}
    private void SlowEnemy() {
        
            FreezeEnemy();
        print("Wth?????");
    }
    public void OnDefeat() {
        //onAnyDefeated(this);
        SlimeHasDied();
        Enemies.Remove(this);
        if (deathEffect != null) { 
        Instantiate(deathEffect, transform);
        }
        //deathEffect.transform.position = transform.position;
        Destroy(gameObject, 2.5f);
        //drop.transform.SetParent(null);
    }
    public void UnsetHit() {
        print("Unset Hit");
        Hit = false;
        Anim.ResetTrigger("Attack 0");
        State=EnemyAiStates.Idle;
    }
    public void CalculateDamage(float addition) {
        if (!dead) {
            Debug.Log((addition));
            Debug.Log((pc.stats.Attack));
            int dmg = (int)Mathf.Clamp(((pc.stats.Attack*addition) - stats.Defense), 1, 999);
            Debug.Log((pc.stats.Attack * addition));
            Debug.Log(stats.Defense);
            HealthLeft -= dmg;
            /*hitSplat.GetComponent<HitText>().Text = dmg.ToString();
            //HitText hitSplat= new HitText();
            //Debug.Log(hitSplat.Text.ToString());

            Instantiate(hitSplat, transform.position, Quaternion.identity);*/
            Hit = true;
            if (HealthLeft <= Health / 4 && !lowHealth) {
                //StartCoroutine(StateControlCoroutine());
                lowHealth = true;
            }
            //OnHit();
        }

    }//(Mathf.Max(1, (int)(Mathf.Pow(stats.Attack - 2.6f * pc.stats.Defense, 1.4f) / 30 + 3))) / n; }
    public void CalculateAttack() {
            pc.stats.HealthLeft -= Mathf.Max(1, stats.Attack);
    }
    public void HitGuard() {
        if (pc.stats.MPLeft > 0) {
            pc.stats.MPLeft -= Mathf.Max(1, stats.Attack);
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
    public void SlimeHasDied() {
        int exp = stats.BaseHealth * stats.ExpYield;
        pc.stats.AddExp(exp);
        if (drop != null) {
            Instantiate(drop, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
            drop.transform.position = transform.position;

        }
        if (soul != null) {
            Instantiate(soul, transform.position + new Vector3(0, 0.18f, 0), Quaternion.identity);
            soul.transform.position = transform.position;
        }

    }

}