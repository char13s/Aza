using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

#pragma warning disable 0649
public class Player : MonoBehaviour
{
    //private bool usingController;
    [Header("Movement")]
    private bool moving;
    public enum Power {Neutral, Heavy, Range }
    private Power style;

    //public float speed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;
    Vector3 directionV;
    Vector2 displacementV;
    Quaternion qTo;

    bool inTeleport;
    #region Attacking
    [Space]
    [Header("Attacking")]
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject stabHitBox;
    [SerializeField] private GameObject rapidHitBox;


    private bool attacking;
    private bool attackState;
    private bool boutaSpin;
    private bool skillButton;

    private bool strenghtened;
    private bool energized;

    private bool lockedOn;
    private bool skillIsActive;
    [SerializeField] GameObject aimmingPoint;
    #endregion
    [Space]
    [Header("rotations")]
    [SerializeField] private GameObject body;
    //public bool right;
    #region Cameras
    [SerializeField] private GameObject mainCam;
    #endregion
    #region Items
    [Space]
    [Header("Items")]
    //public List<Items> inventory;
    [SerializeField] private GameObject demonSword;
    [SerializeField] private GameObject demonSwordBack;
    //[SerializeField] private GameObject demonFistLeft;
    //[SerializeField] private GameObject demonFistRight;
    //[SerializeField] private GameObject scythe;
    //[SerializeField] private GameObject angelSword;
    //[SerializeField] private GameObject fakeAngelSword;
    //[SerializeField] private GameObject guitar;
    //[SerializeField] private GameObject bow;
    //[SerializeField] private GameObject attackBow;
    //[SerializeField] private GameObject mask;
    //[SerializeField] private GameObject trail;
    //[SerializeField] private GameObject shield;
    //[SerializeField] private GameObject shieldBack;
    //[SerializeField] private GameObject axe;
    //[SerializeField] private GameObject outaBed;
    #endregion
    #region Animation States
    [Space]
    [Header("Animation States")]

    [SerializeField] private Transform movementBone;
    [SerializeField] private Transform headBone;
    //[SerializeField] private Vector3 moveBoneForward = new Vector3(0, 0, 1);
    [SerializeField] private Vector3 moveBoneRight = new Vector3(1, 0, 0);
    private bool attack;
    private bool airAttack;

    private bool grounded;
    private bool blocking;
    private bool transforming;

    private bool guard;
    private bool hit;
    private bool dead;
    private int direction;
    private bool dodge;
    private bool dash;
    private bool charging;

    private int skillId;
    private bool powerUp;

    private int cmdInput;

    private bool poweredUp;
    private bool jumping;
    private int cinemations;

    private int combatAnimations;
    private int guardAnimations;

    private bool spinAttack;
    private float speedInc;

    private int shootLayer;
    private int guardLayer;
    private int fallingLayer;
    private int airCombo;

    private bool hasTarget;

    private int magicLevel;
    #endregion
    #region Random stuff
    [Space]
    [Header("OtherFunctions")]
    private AudioSource sfx;
    private AudioSource clothesSfx;
    private PlayerEffects effects;
    private bool pause;
    private bool targeting;

    private byte timer;
    private bool loaded;
    private int arrowType;
    [Space]
    [Header("References To Things on Zend")]
    [SerializeField] private GameObject zend;

    [SerializeField] private GameObject devilFoot;

    [SerializeField] private GameObject zaWarudosRange;


    [Space]
    [Header("OtherWorldyFeatures")]
    [SerializeField] private GameObject zendsRHorn;
    [SerializeField] private GameObject zendsLHorn;
    
    [SerializeField] private GameObject halo;

    [Space]
    [Header("HitBoxes")]
    [SerializeField] private GameObject aoeHitbox;
    //[SerializeField] private GameObject forwardHitbox;
    //[SerializeField] private GameObject shieldHitBox;
    #endregion
    [Space]
    private static Player instance;
    private Coroutine guardCoroutine;
    private Coroutine hitDefuse;
    private Coroutine dodgeCoroutine;
    private Coroutine mpDrain;
    private Coroutine staminaRec;

    private int hitCounter;
    //private Vector3 delta;
    private bool perfectGuard;
    //private NavMeshAgent nav;
    private PlayerBattleSceneMovement battleMode;
    private FreeFallMovement freeFallMode;
    private Animator anim;
    private Animator topAnim;
    private CharacterController charCon;
    private Vector3 displacement;//world space 
    private int skullMask;
    private int bulbs;
    private PlayerBodyObjects playerBody;
    private BasicHeadController headController;
    private PlayerMovement playerMove;
    #region Constructors
    internal Inventory items = new Inventory();
    internal Stats stats = new Stats();
    internal StatusEffects status = new StatusEffects();


    //private bool doubleJump;
    private bool boosting;
    private bool teleportTriggered;
    private bool timeStopped;
    private bool stopTime;
    private bool locked;
    private float attackCharge;
    private bool drawn;

    #endregion
    #region Events
    public static event UnityAction onPlayerDeath;
    public static event UnityAction onPlayerEnabled;
    public static event UnityAction playerIsLockedOn;
    public static event UnityAction notAiming;

    public static event UnityAction zaWarudo;
    public static event UnityAction timeHasBegunToMove;
    public static event UnityAction weaponSwitch;
    public static event UnityAction attackModeUp;
    public static event UnityAction findClosestEnemy;
    public static event UnityAction unlocked;

    public static event UnityAction<AudioClip> sendSfx;
    public static event UnityAction<int> playSound;
    public static event UnityAction<int> formChange;

    public static event UnityAction<int> zoom;

    #endregion
    //Optimize these to use only one Animation parameter in 9/14
    #region Getters and Setters
    public bool Grounded { get => grounded; set { grounded = value;  } }//anim.SetBool("Grounded", grounded);
    public bool Guard { get => guard; set { guard = value; anim.SetBool("Guard", guard); } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public GameObject Body { get => body; set => body = value; }
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); } }
    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); if (dead) { OnDeath(); } else { } } }
    public int Direction { get => direction; set { direction = value; anim.SetInteger("Direction", direction); } }//used for determining base lock on directions with no target in player battle scene mov.
    public bool Pause { get => pause; set { pause = value; if (pause) { Time.timeScale = 0; } else { Time.timeScale = 1; } } }
    public bool Loaded { get => loaded; set { loaded = value;/*Nav.enabled=true*/  } }//determines wheter scene has been loaded
    public PlayerBattleSceneMovement BattleMode { get => battleMode; set => battleMode = value; }
    public GameObject DemonSword { get => demonSword; set { demonSword = value; } }
    public GameObject HitBox { get => hitBox; set { hitBox = value; } }

    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("Skill ID", skillId); if (skillId == 0) { SkillIsActive = false; } } }


    public Animator Anim { get => anim; set => anim = value; }

    public bool PerfectGuard { get => perfectGuard; set => perfectGuard = value; }
    //public GameObject ForwardHitbox { get => forwardHitbox; set => forwardHitbox = value; }
    //public GameObject FireTrail { get => fireTrail; set => fireTrail = value; }//might have to get rid of
    public GameObject AoeHitbox { get => aoeHitbox; set => aoeHitbox = value; }

    public bool LockedOn { get => lockedOn; set { lockedOn = value; anim.SetBool("Lock", lockedOn); anim.SetBool("AttackStance", lockedOn); if (!LockedOn) { if (unlocked != null) unlocked();} } }//actual player locked on

    public bool SkillIsActive { get => skillIsActive; set { skillIsActive = value; if (skillIsActive) { Guard = false; } } }
    public int CmdInput { get => cmdInput; set { cmdInput = value; anim.SetInteger("CommandInput", cmdInput); } }//see if you can get rid of this

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool PowerUp { get => powerUp; set { powerUp = value; anim.SetBool("PowerUp", powerUp); } }
    public AudioSource Sfx { get => sfx; set => sfx = value; }
    public bool PoweredUp { get => poweredUp; set { poweredUp = value; } }
    public bool Transforming { get => transforming; set => transforming = value; }
    public GameObject AimmingPoint { get => aimmingPoint; set => aimmingPoint = value; }
    public bool Jumping { get => jumping; set { jumping = value; anim.SetBool("Jumping", value); } }

    private bool castItem;
    private bool endure;

    public int Cinemations { get => cinemations; set { cinemations = value; anim.SetInteger("Cinemaitions", cinemations); } }

    public bool TeleportTriggered { get => teleportTriggered; set => teleportTriggered = value; }

    public bool StopTime { get => stopTime; set { stopTime = value; anim.SetBool("TimeStop", stopTime); } }
    public GameObject StabHitBox { get => stabHitBox; set => stabHitBox = value; }

    public int CombatAnimations { get => combatAnimations; set { combatAnimations = value; anim.SetInteger("CombatAnimation", combatAnimations); } }

    public GameObject RapidHitBox { get => rapidHitBox; set => rapidHitBox = value; }
    public int GuardAnimations { get => guardAnimations; set { guardAnimations = value; anim.SetInteger("GuardAnimations", guardAnimations); } }

    public float JumpForce { get => jumpForce; set => jumpForce = value; }

    //public GameObject ShieldHitBox { get => shieldHitBox; set => shieldHitBox = value; }
    public AudioSource ClothesSfx { get => clothesSfx; set => clothesSfx = value; }
    public bool Drawn { get => drawn; set { drawn = value; anim.SetBool("Drawn", drawn); } }

    public bool CastItem { get => castItem; set { castItem = value; anim.SetBool("Cast", castItem); } }
    public Transform MovementBone { get => movementBone; set => movementBone = value; }

    public int ArrowType { get => arrowType; set => arrowType = value; }
    public bool SkillButton { get => skillButton; set => skillButton = value; }
    public float SpeedInc { get => speedInc; set => speedInc = value; }
    public int AirCombo { get => airCombo; set { airCombo = value; anim.SetInteger("AirCombo", airCombo); } }

    public bool Blocking { get => blocking; set => blocking = value; }
    public GameObject MainCam { get => mainCam; set => mainCam = value; }
    public bool AttackState { get => attackState; set { attackState = value; } }

    public CharacterController CharCon { get => charCon; set => charCon = value; }
    public bool Attack { get => attack; set { attack = value; anim.SetBool("Attacking", attack); } }
    public PlayerMovement PlayerMove { get => playerMove; set => playerMove = value; }
    public bool AirAttack { get => airAttack; set => airAttack = value; }
    public PlayerBodyObjects PlayerBody { get => playerBody; set => playerBody = value; }
    public PlayerEffects Effects { get => effects; set => effects = value; }
    public Animator TopAnim { get => topAnim; set => topAnim = value; }
    public bool Strenghtened { get => strenghtened; set => strenghtened = value; }
    public bool Energized { get => energized; set => energized = value; }
    public bool HasTarget { get => hasTarget; set => hasTarget = value; }
    public int MagicLevel { get => magicLevel; set { magicLevel = value; anim.SetInteger("MagicLevel",magicLevel); } }

    public bool Dodge { get => dodge; set => dodge = value; }
    public Power Style { get => style; set => style = value; }
    public FreeFallMovement FreeFallMode { get => freeFallMode; set => freeFallMode = value; }
    public bool Dash { get => dash; set { dash = value;anim.SetBool("Dash", dash); } }

    //public Rigidbody Rbody { get => rbody; set => rbody = value; }

    //public GameObject GroundChecker { get => groundChecker; set => groundChecker = value; }
    #endregion
    public static Player GetPlayer() => instance.GetComponent<Player>();
    // Start is called before the first frame update
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        
        PlayerMove = GetComponent<PlayerMovement>();
        ClothesSfx = zend.GetComponent<AudioSource>();
        Anim = zend.GetComponent<Animator>();
        TopAnim = GetComponent<Animator>();
        //rbody = GetComponent<Rigidbody>();
        charCon = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
        battleMode = GetComponent<PlayerBattleSceneMovement>();
        headController = GetComponent<BasicHeadController>();
        Effects = GetComponent<PlayerEffects>();
        PlayerBody = GetComponent<PlayerBodyObjects>();
        FreeFallMode = GetComponent<FreeFallMovement>();
    }

    void Start() {
        //Stats.onStaminaChange+=StartCoroutine(StaminaRec());
        stats.Start();
        MagicLevel = 1;
        items.Start();
        Stats.onHealthChange += CheckPlayerHealth;
        guardLayer = anim.GetLayerIndex("GuardLayer");
        shootLayer = anim.GetLayerIndex("Shooting Layer");
        fallingLayer= anim.GetLayerIndex("Fall Layer");
    }
    
    private void OnEnable() {
        sfx = GetComponent<AudioSource>();
        mainCam = GameManager.GetManager().Camera;
        #region Event Subs
        Stats.sendSpeed += IncreaseSpeed;
        Enemy.onHit += MpRegain;
        Enemy.guardBreak += GuardBreak;
        LevelManager.levelFinished += SetInTeleport;

        GameController.respawn += RestoreHealth;
        GroundChecker.landed += SoundEffects;

        DoubleJump.doubleJump += SoundEffects;
        SpellTag.triggerZaWarudo += ZaWarudo;
        JudgementCut.stop += ZaWarudo;
        DrawSword.hideSword += DrawSwordOut;
        global::Dash.dash += SoundEffects;
        EnemyHitBox.hit += OnHit;
        ReactionRange.dodged += ZaWarudo;

        UiManager.disablePlayer += DisableBody;
        UiManager.unsealPlayerInput += EnableBody;

        GroundSound.sendSound += GroundSoundManagement;
        FormSwitch.inviciblity += Endure;
        //MovingStates.returnSpeed += MoveBro;
        //BaseBehavoirs.grounded += ZeroVelocity;
        PoisonLake.poisoned += TakeDamage;
        PlayerAnimationEvents.shoot += ShootShadow;
        ShootBehavior.shoot += ShootLayer;
        SwitchToFallGame.switchToFall += SwitchToFallingLayer;
        TimelineTrigger.disableCodeMove += SwitchToFallingLayer;
        AttackStates.sendAttack += RecieveAttack;
        LevelManager.levelTransition += OnLevelTransition;
        #region Item subs
        ItemData.mask += PowerUpp;
        #endregion
        #endregion
        #region Power HookUps
        AngelicRelic.teleportTo += Teleportto;
        DefensePowers.defense += Block;
        #endregion
        #region Animation Events
        PlayerAnimationEvents.kickback += AddForceToPlayer;
        #endregion
        #region Skill Tree Subs
        MagicComboExtend.sendUpgrade += UpMagicMagicCombo;
        #endregion
        if (onPlayerEnabled != null) {
            onPlayerEnabled();
        }
    }
    private void OnDisable() {
        sfx = GetComponent<AudioSource>();
        MagicComboExtend.sendUpgrade -= UpMagicMagicCombo;
        #region Event Subs
        Stats.sendSpeed -= IncreaseSpeed;
        Enemy.onHit -= MpRegain;
        Enemy.guardBreak -= GuardBreak;
        LevelManager.levelFinished -= SetInTeleport;

        GameController.respawn -= RestoreHealth;
        GroundChecker.landed -= SoundEffects;

        DoubleJump.doubleJump -= SoundEffects;
        SpellTag.triggerZaWarudo -= ZaWarudo;
        JudgementCut.stop -= ZaWarudo;
        DrawSword.hideSword -= DrawSwordOut;
        global::Dash.dash -= SoundEffects;
        EnemyHitBox.hit -= OnHit;
        ReactionRange.dodged -= ZaWarudo;

        UiManager.disablePlayer -= DisableBody;
        UiManager.unsealPlayerInput -= EnableBody;

        GroundSound.sendSound -= GroundSoundManagement;
        FormSwitch.inviciblity -= Endure;
        //MovingStates.returnSpeed += MoveBro;
        //BaseBehavoirs.grounded += ZeroVelocity;
        PoisonLake.poisoned -= TakeDamage;
        PlayerAnimationEvents.shoot -= ShootShadow;
        ShootBehavior.shoot -= ShootLayer;
        SwitchToFallGame.switchToFall -= SwitchToFallingLayer;
        TimelineTrigger.disableCodeMove -= SwitchToFallingLayer;
        AttackStates.sendAttack -= RecieveAttack;
        LevelManager.levelTransition -= OnLevelTransition;
        #region Item subs
        ItemData.mask -= PowerUpp;
        #endregion
        #endregion
        #region Power HookUps
        AngelicRelic.teleportTo -= Teleportto;
        DefensePowers.defense -= Block;
        #endregion
        #region 
        PlayerAnimationEvents.kickback -= AddForceToPlayer;
        #endregion
    }
    // Update is called once per frame
    void FixedUpdate() {
        // Move();
        //transform.position = transform.position +  new Vector3(0, 0, 1);
    }
    #region Helper Methods
    private void CheckPlayerHealth() {
        if (stats.HealthLeft <= 0 && !dead) { Dead = true; }
    }

    #endregion

    #region new code
    private void RecieveAttack() {
        Attack = false;
    }
    #endregion

    #region Ability Functions
    private void Teleportto() {
        inTeleport = true;
        if (battleMode.EnemyTarget.Grounded) {
            print("In the air i go");
            transform.position = battleMode.EnemyTarget.gameObject.transform.position + new Vector3(0, 4, -0.5f);
        }
        else {
            transform.position = battleMode.EnemyTarget.gameObject.transform.position + new Vector3(0, 0.5f, -0.5f);
            print("In the air i go not");
        }
        StartCoroutine(WaitToLoad(false));
    }
    private void SetInTeleport(bool val) {
        StartCoroutine(WaitToLoad(val));
    }
    private IEnumerator WaitToLoad(bool val) {
        yield return null;
        yield return null;
        inTeleport = val; 

    }
    private void ShootShadow() {
        Instantiate(Effects.ShadowShot, playerBody.LeftHand.transform.position, Quaternion.identity);
    }
    public void ShootLayer(int val) {
        anim.SetLayerWeight(shootLayer, val);
    }
    private void Block(bool val) => Guard = val;
    #endregion

    #region Physics added thru animation events
    private void AddForceToPlayer(float move) {//For Dodge movement
        //rBody.AddForce(transform.forward * -move, ForceMode.Impulse);

        CombatAnimations = 0;
    }
    #endregion
    #region Layer control
    private void SwitchToFallingLayer(int val) {
        switch (val) {
            case 0:
                CodeMovementControls(false, true);
                break;
            case 2:
                CodeMovementControls(true, false);
                break;
            default:
                CodeMovementControls(false, false);
                break;
           
        }
        //transform.rotation = new Quaternion(87,0,0,0);
        anim.SetLayerWeight(fallingLayer,val);
    }
    private void CodeMovementControls(bool freeVal,bool moaveVal) {
        FreeFallMode.enabled = freeVal;
        playerMove.enabled = moaveVal;
    }
    #endregion
    #region Time Stuff
    private void ZaWarudo() {
        Debug.Log("Za BITCH");
        
            Debug.Log("ZA HOE");
            //zaWarudosRange.SetActive(true);
            Debug.Log("za warudo?");
            timeStopped = true;
            //StopTime = true;
            Time.timeScale = 0.1f;
            if (zaWarudo != null) {
                zaWarudo();
            }
        zoom.Invoke(4);
        StartCoroutine(ResetTimeStop());
        StartCoroutine(UndoZaWarudo());
        
    }
    private IEnumerator UndoZaWarudo() {
        YieldInstruction wait = new WaitForSeconds(1f);
        yield return wait;
        zaWarudosRange.SetActive(false);
        if (timeHasBegunToMove != null) {
            timeHasBegunToMove();
        }
    }
    #endregion
    private void OnDeath() {
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
    }

    private void GuardBreak() {
        GuardAnimations = 3;
    }
    #region Unused Methods
    private void SetHeadWeight(float result) {
        headController.Weight = result;
    }
    #endregion
    private void OnLevelTransition(bool val) {
        playerMove.enabled = val;
    }
    #region Coroutines

    private IEnumerator SetLayerWeightCoroutine(int layerIndex, float weight, float duration, UnityAction<float> onFade) {
        float localTime = 0;
        float start = anim.GetLayerWeight(layerIndex);
        float deltaWeight = weight - start;
        float result = start;
        while (localTime < duration) {
            result = start + (localTime / duration) * (deltaWeight);
            anim.SetLayerWeight(layerIndex, result);
            if (onFade != null) {
                onFade(result);
            }
            yield return null;
            localTime += Time.deltaTime;

        }
        anim.SetLayerWeight(layerIndex, weight);
        if (onFade != null) {
            onFade(weight);
        }
    }
    private IEnumerator StaminaRec() {

        while (isActiveAndEnabled) {
            YieldInstruction wait = new WaitForSeconds(3);
            yield return wait;

            if (stats.MPLeft < stats.MP) {
                stats.MPLeft += 5;
            }
        }

    }
    private IEnumerator MpDrain() {
        while (isActiveAndEnabled) {
            YieldInstruction wait = new WaitForSeconds(3f);
            yield return wait;
            stats.MPLeft--;
            if (stats.MPLeft == 0) {
                //Deform();
            }
        }

    }
    private IEnumerator ResetTimeStop() {
        //YieldInstruction wait = new WaitForSeconds(2);

        yield return new WaitForSecondsRealtime(2);
        zoom.Invoke(7);
        timeStopped = false;
        Time.timeScale = 1;
    }
    private IEnumerator SetLayerWait() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        //anim.SetLayerWeight(legsLayer, 1);

    }
    #endregion
    #region Inputs
    public void TargetingLogic(bool val) {
        if (val) {
            LockedOn = true;
            if (playerIsLockedOn != null) {
                playerIsLockedOn();
            }
            findClosestEnemy.Invoke();
        }
        else {
            notAiming.Invoke();
            LockedOn = false;
        }
    }

    #endregion

    #region Event handlers

    private void IncreaseSpeed() {
        anim.SetFloat("SpeedInc",stats.Speed);
    }
    private void DrawSwordOut(AudioClip sound) {

        demonSwordBack.SetActive(false);
        DemonSword.SetActive(true);
        drawn = true;
        sfx.PlayOneShot(sound);
    }
    private void Endure(bool val) {
        endure = val;
    }
    private void OnHit() {
        if (!hit && !endure) {
            Hit = true;
        }
    }
    private void EnableBody() {
        Body.SetActive(true);
    }
    private void DisableBody() {
        Body.SetActive(false);
    }
    private void MpRegain() {
        stats.MPLeft += 2;
    }
    private void GroundSlamForce(float force) {
        /// RBody.mass = force;
    }
    private void Respawn() {
        transform.position = GameController.GetGameController().Spawn.transform.position;
    }
    private void TakeDamage(int damage) {
        stats.HealthLeft -= damage;
    }
    private void RestoreHealth() {
        Dead = false;
        stats.HealthLeft = stats.Health;
    }
    private void PowerUpp() {
        mpDrain = StartCoroutine(MpDrain());
        //mask.SetActive(true);
        PoweredUp = true;
        PowerUp = true;
    }


    //private void ZeroVelocity() {
    //   //rBody.velocity = new Vector3(0, 0, 0);
    //}
    #endregion
    #region Skill tree Upgrades
    private void UpMagicMagicCombo() {
        MagicLevel++;
        print("Magic level upgraded");
    }
    
    #endregion
    #region Sounds
    //private void LandingSound(AudioClip sound) {
    //    sfx.PlayOneShot(sound);
    //}
    private void GroundSoundManagement(AudioClip sound) {
        if (sendSfx != null) {
            sendSfx(sound);
        }
        //ClothesSfx.clip = sound;
    }
    private void SoundEffects(AudioClip sound) {
        if (sendSfx != null) {
            sendSfx(sound);
        }
    }
    #endregion
}
