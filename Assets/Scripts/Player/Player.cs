using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using XInputDotNetPure;
#pragma warning disable 0649
public class Player : MonoBehaviour {
    //private bool usingController;
    [Header("Movement")]
    private Vector3 direction, moveDirection;
    private Vector2 displacement;
    private bool moving;
    private Quaternion qTo;
    [SerializeField] private float rotationSpeed;
    private bool weak;
    private int style;
    private bool hasBoth;
    //public float speed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float burstForce;

    private float l;
    private bool cantDoubleJump = true;
    private int money;
    private bool inputSealed;
    #region Attacking
    [Space]
    [Header("Attacking")]
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject fistHitBox;
    [SerializeField] private GameObject stabHitBox;
    [SerializeField] private GameObject rapidHitBox;
    [SerializeField] private GameObject katanaHitbox;
    [SerializeField] private GameObject scytheHitBox;
    [SerializeField] private GameObject woodenSwordHitBox;


    private bool attacking;
    private bool boutaSpin;
    private bool skillButton;
    private bool lockedOn;
    [SerializeField] private GameObject swordSpawn;
    [SerializeField] private GameObject swordDSpawn;

    private bool skillIsActive;
    [SerializeField] GameObject aimmingPoint;
    private int weapon;
    private int weaponMax;
    private int weaponMin;
    #endregion
    [Space]
    [Header("rotations")]
    [SerializeField] private GameObject body;
    //public bool right;
    #region Cameras
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject cameraPoint;
    #endregion
    #region Items
    [Space]
    [Header("Items")]
    //public List<Items> inventory;
    [SerializeField] private GameObject demonSword;
    [SerializeField] private GameObject demonSwordBack;
    [SerializeField] private GameObject demonFistLeft;
    [SerializeField] private GameObject demonFistRight;
    [SerializeField] private GameObject scythe;
    [SerializeField] private GameObject angelSword;
    [SerializeField] private GameObject fakeAngelSword;
    [SerializeField] private GameObject guitar;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject attackBow;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shieldBack;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject outaBed;
    #endregion
    #region Animation States
    [Space]
    [Header("Animation States")]
    [SerializeField] private int archeryLayerIndex = 1;
    [SerializeField] private Transform movementBone;
    [SerializeField] private Transform headBone;
    //[SerializeField] private Vector3 moveBoneForward = new Vector3(0, 0, 1);
    [SerializeField] private Vector3 moveBoneRight = new Vector3(1, 0, 0);

    private bool grounded;
    private bool transforming;
    
    private bool leftDash;
    private bool rightDash;
    private bool guard;
    private bool hit;
    private bool dead;
    private int motionDirection;

    private bool charging;
    
    private int skillId;
    private bool powerUp;
    private bool sleep;
    private int cmdInput;
    private int animations;
    private bool bowUp;
    private bool poweredUp;
    private bool jumpSeal;
    private bool jumping;
    private int cinemations;
    private bool inHouse;
    private int combatAnimations;
    private int guardAnimations;
    private bool doubleJump;
    private bool spinAttack;
    private bool withdraw;
    private bool demonFlame;
    private bool flying;

    private int demonLayer;
    private int angelLayer;   
    private int legsLayer;
    private int guardLayer;
    private int castLayer;
    private int drawSwordLayer;

    private bool longRangeAttack;
    #endregion
    #region Random stuff
    [Space]
    [Header("OtherFunctions")]
    private Rigidbody rBody;
    private AudioSource sfx;
    private AudioSource clothesSfx;
    private bool pause;
    private bool targeting;

    private byte timer;
    private bool loaded;
    private int arrowType;
    [Space]
    [Header("References To Things on Zend")]
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject arrowPoint;

    [SerializeField] private GameObject groundChecker;
    [SerializeField] private GameObject battleCamTarget;
    [SerializeField] private GameObject zend;
    [SerializeField] private GameObject zendHair;
    [SerializeField] private GameObject zendHead;
    [SerializeField] private GameObject fireTrail;
    [SerializeField] private GameObject fireCaster;
    [SerializeField] private GameObject devilFoot;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject zaWarudosRange;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private GameObject centerBodyPoint;
    [SerializeField] private GameObject highPoint;
    [SerializeField] private GameObject hitPoint;
    [SerializeField] private GameObject jumpPoint;
    [SerializeField] private GameObject woodenSword;

    [Space]
    [Header("OtherWorldyFeatures")]
    [SerializeField] private GameObject zendsRHorn;
    [SerializeField] private GameObject zendsLHorn;
    [SerializeField] private GameObject lightning;
    [SerializeField] private GameObject fireAura;
    [SerializeField] private GameObject halo;
    [SerializeField] private GameObject fireTrailR;
    [SerializeField] private GameObject fireTrailL;
    [SerializeField] private GameObject demonScabbard;
    [SerializeField] private GameObject angelScabbard;
    [SerializeField] private GameObject angelSwordSide;

    //[SerializeField] private GameObject reactionRange;
    [Space]
    [Header("HitBoxes")]
    [SerializeField] private GameObject AoeHitbox;
    [SerializeField] private GameObject forwardHitbox;
    [SerializeField] private GameObject shieldHitBox;
    #endregion
    [Space]
    [Header("Buttons")]
    [SerializeField] private SkillButton triangle;
    [SerializeField] private SkillButton circle;
    [SerializeField] private SkillButton square;
    [SerializeField] private SkillButton x;
    [SerializeField] private SpellTagSlot L1Triangle;
    [SerializeField] private SpellTagSlot L1Circle;
    [SerializeField] private SpellTagSlot L1Square;
    [SerializeField] private SpellTagSlot L1X;
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
    private Animator anim;
    //private Vector3 displacement;//world space 
    private int skullMask;
    private int bulbs;

    private BasicHeadController headController;
    #region Constructors
    internal Inventory items = new Inventory();
    internal Inventory weaponInvent = new Inventory();
    internal Inventory shieldInvent = new Inventory();
    internal Inventory maskInvent = new Inventory();
    internal Stats stats = new Stats();
    private AxisButton dPadUp = new AxisButton("DPad Up");
    private AxisButton dPadRight = new AxisButton("DPad Right");
    private AxisButton R2 = new AxisButton("R2");

    private AxisButton L2 = new AxisButton("L2");


    internal StatusEffects status = new StatusEffects();
    //private bool doubleJump;
    private bool boosting;
    private bool teleportTriggered;
    private bool timeStopped;
    private bool stopTime;
    private bool pressed;
    private float weaponAmount = 1;
    private bool locked;
    private float attackCharge;
    private bool drawn;

    #endregion
    #region Events
    public static event UnityAction aiming;
    public static event UnityAction onPlayerDeath;
    public static event UnityAction onPlayerEnabled;
    public static event UnityAction playerIsLockedOn;
    public static event UnityAction onCharacterSwitch;
    public static event UnityAction notAiming;
    public static event UnityAction lockOn;
    public static event UnityAction kintoun;
    public static event UnityAction kryll;
    public static event UnityAction battleOn;
    public static event UnityAction notSleeping;
    public static event UnityAction cancelPaused;
    public static event UnityAction zaWarudo;
    public static event UnityAction timeHasBegunToMove;
    public static event UnityAction weaponSwitch;
    public static event UnityAction attackModeUp;
    public static event UnityAction findClosestEnemy;
    public static event UnityAction unlocked;
    public static event UnityAction<bool> skills;

    public static event UnityAction<AudioClip> sendSfx;
    public static event UnityAction<int> archery;
    public static event UnityAction dpadUp;
    public static event UnityAction dpadDown;
    public static event UnityAction dpadRight;
    public static event UnityAction dpadLeft;
    public static event UnityAction<int> playSound;
    public static event UnityAction<int> formChange;
    public static event UnityAction<bool> flight;
    public static event UnityAction onCircle;
    #endregion
    //Optimize these to use only one Animation parameter in 9/14
    #region Getters and Setters
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); WeaponManagement(); if (grounded) { /*RBody.isKinematic = true; /*nav.enabled = true;*/ SecondJump = false; CantDoubleJump = true; } } }
    public bool LeftDash { get => leftDash; set { leftDash = value; anim.SetBool("LeftDash", leftDash); } }
    public bool RightDash { get => rightDash; set { rightDash = value; anim.SetBool("RightDash", rightDash); } }
    public bool Guard { get => guard; set { guard = value; if (value) Moving = false; anim.SetBool("Guard", guard); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); WeaponManagement(); if (attackModeUp != null) { attackModeUp(); } } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public GameObject Body { get => body; set => body = value; }
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); } }
    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); if (dead) { OnDeath(); } else { } } }
    public int MotionDirection { get => motionDirection; set { motionDirection = value; anim.SetInteger("Direction", motionDirection); } }
    public bool Pause { get => pause; set { pause = value; if (pause) { Time.timeScale = 0; } else { Time.timeScale = 1; } } }
    public bool Loaded { get => loaded; set { loaded = value;/*Nav.enabled=true*/  } }
    public PlayerBattleSceneMovement BattleMode { get => battleMode; set => battleMode = value; }
    public GameObject DemonSword { get => demonSword; set { demonSword = value; if (!demonSword.activeSelf) { drawn = false; } } }
    public GameObject HitBox { get => hitBox; set { hitBox = value; } }

    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("Skill ID", skillId); if (skillId == 0) { SkillIsActive = false; } } }
    public Rigidbody Rbody { get => rBody; set => rBody = value; }
    
    public Animator Anim { get => anim; set => anim = value; }
    
    public bool PerfectGuard { get => perfectGuard; set => perfectGuard = value; }
    public GameObject ForwardHitbox { get => forwardHitbox; set => forwardHitbox = value; }
    public GameObject FireTrail { get => fireTrail; set => fireTrail = value; }
    public GameObject AoeHitbox1 { get => AoeHitbox; set => AoeHitbox = value; }
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", animations); LegControl(); } }

    public bool LockedOn { get => lockedOn; set { lockedOn = value; anim.SetBool("Lock", lockedOn); if (!LockedOn) { if (unlocked != null) unlocked(); MotionDirection = 0; } } }

    public bool SkillIsActive { get => skillIsActive; set { skillIsActive = value; if (skillIsActive) { Guard = false; } } }
    public int CmdInput { get => cmdInput; set { cmdInput = value; anim.SetInteger("CommandInput", cmdInput); } }

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool PowerUp { get => powerUp; set { powerUp = value; anim.SetBool("PowerUp", powerUp); } }

    public GameObject ZendHair { get => zendHair; set => zendHair = value; }
    public AudioSource Sfx { get => sfx; set => sfx = value; }
    public int Money { get => money; set => money = value; }
    public bool PoweredUp { get => poweredUp; set { poweredUp = value; } }

    public bool BowUp { get => bowUp; set { bowUp = value; anim.SetBool("BowUp", bowUp); } }

    public bool Transforming { get => transforming; set => transforming = value; }
    public GameObject AttackBow { get => attackBow; set => attackBow = value; }
    public GameObject AimmingPoint { get => aimmingPoint; set => aimmingPoint = value; }
    public bool InputSealed { get => inputSealed; set => inputSealed = value; }
    public bool Sleeping { get => sleep; set { sleep = value; anim.SetBool("Sleep", value); } }

    public bool JumpSeal { get => jumpSeal; set => jumpSeal = value; }
    public bool Jumping { get => jumping; set { jumping = value; anim.SetBool("Jumping", value); } }

    public bool Boosting { get => boosting; set { boosting = value; anim.SetBool("Dashing", boosting); } }

    private bool dash;
    private bool zendSpace;
    private bool leftPressed;
    private bool castItem;
    private bool strongAttack;
    private bool lightAttack;
    private bool endure;
    

    public GameObject DevilFoot { get => devilFoot; set => devilFoot = value; }
    public GameObject LeftHand { get => leftHand; set => leftHand = value; }
    public GameObject RightHand { get => rightHand; set => rightHand = value; }
    public int Cinemations { get => cinemations; set { cinemations = value; anim.SetInteger("Cinemaitions", cinemations); } }

    public bool TeleportTriggered { get => teleportTriggered; set => teleportTriggered = value; }
    
    public int Weapon { get => weapon; set { weapon = Mathf.Clamp(value, weaponMin, weaponMax); WeaponManagement(); anim.SetInteger("Weapon", weapon); if (weaponSwitch != null) { weaponSwitch(); } } }

    public GameObject FistHitBox { get => fistHitBox; set => fistHitBox = value; }
    public bool StopTime { get => stopTime; set { stopTime = value; anim.SetBool("TimeStop", stopTime); } }

    public GameObject Trail { get => trail; set => trail = value; }
    public bool SecondJump { get => doubleJump; set { doubleJump = value; anim.SetBool("DoubleJump", doubleJump); } }
    public bool InHouse { get => inHouse; set { inHouse = value; if (inHouse) { DemonSword.SetActive(false); } } }//demonSwordBack.SetActive(true);

    public GameObject BattleCamTarget { get => battleCamTarget; set => battleCamTarget = value; }
    public float BurstForce { get => burstForce; set => burstForce = value; }
    public GameObject CenterPoint { get => centerPoint; set => centerPoint = value; }
    public GameObject HitPoint { get => hitPoint; set => hitPoint = value; }
    public GameObject StabHitBox { get => stabHitBox; set => stabHitBox = value; }
    public GameObject CenterBodyPoint { get => centerBodyPoint; set => centerBodyPoint = value; }
    public int CombatAnimations { get => combatAnimations; set { combatAnimations = value; anim.SetInteger("CombatAnimation", combatAnimations); } }

    public GameObject RapidHitBox { get => rapidHitBox; set => rapidHitBox = value; }
    public int GuardAnimations { get => guardAnimations; set { guardAnimations = value; anim.SetInteger("GuardAnimations", guardAnimations); } }

    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public GameObject JumpPoint { get => jumpPoint; set => jumpPoint = value; }
    public bool CantDoubleJump { get => cantDoubleJump; set => cantDoubleJump = value; }
    public GameObject ShieldHitBox { get => shieldHitBox; set => shieldHitBox = value; }
    public AudioSource ClothesSfx { get => clothesSfx; set => clothesSfx = value; }
    public bool Drawn { get => drawn; set { drawn = value; anim.SetBool("Drawn", drawn); } }

    public bool CastItem { get => castItem; set { castItem = value; anim.SetBool("Cast", castItem); } }
    public Transform MovementBone { get => movementBone; set => movementBone = value; }
    public GameObject LeftPoint { get => leftPoint; set => leftPoint = value; }
    public int ArrowType { get => arrowType; set => arrowType = value; }
    public GameObject ArrowPoint { get => arrowPoint; set => arrowPoint = value; }
    public GameObject KatanaHitbox { get => katanaHitbox; set => katanaHitbox = value; }
    public GameObject Scythe { get => scythe; set => scythe = value; }
    public GameObject AngelSword { get => angelSword; set => angelSword = value; }
    public GameObject ScytheHitBox { get => scytheHitBox; set => scytheHitBox = value; }
    public float L { get => l; set { l = value; anim.SetFloat("LStick", l); } }

    public bool SpinAttack { get => spinAttack; set { spinAttack = value; anim.SetBool("SpinAttack", spinAttack); } }

    public GameObject HighPoint { get => highPoint; set => highPoint = value; }
    public bool BoutaSpin { get => boutaSpin; set => boutaSpin = value; }
    public bool Weak { get => weak; set { weak = value; anim.SetBool("Weak", weak); } }

    public GameObject WoodenSwordHitBox { get => woodenSwordHitBox; set => woodenSwordHitBox = value; }
    public int LegsLayer { get => legsLayer; set => legsLayer = value; }
    public bool Withdraw1 { get => withdraw; set { withdraw = value; anim.SetBool("Withdraw", withdraw); } }

    public bool LightAttack { get => lightAttack; set { lightAttack = value; anim.SetBool("LightAttack", lightAttack); } }
    public bool StrongAttack { get => strongAttack; set { strongAttack = value; anim.SetBool("StrongAttack", strongAttack); } }

    public GameObject FakeAngelSword { get => fakeAngelSword; set => fakeAngelSword = value; }
    public int Style { get => style; set { style = value; anim.SetInteger("Style", style); } }

    public int SkullMask { get => skullMask; set => skullMask = value; }
    public bool Charging { get => charging; set { charging = value;anim.SetBool("Charging",charging); } }
    public int Bulbs { get => bulbs; set => bulbs = value; }
    public GameObject WoodenSword { get => woodenSword; set => woodenSword = value; }
    public bool DemonFlame { get => demonFlame; set { demonFlame = value;anim.SetBool("DemonFlame",demonFlame); if (demonFlame) { DemonUp(); } else { Base();Rbody.useGravity = true; } } }

    public bool Flying { get => flying; set { flying = value; anim.SetBool("Flying",flying);if (flight != null) { flight(flying); } } }

    public bool LongRangeAttack { get => longRangeAttack; set { longRangeAttack = value;anim.SetBool("Range",longRangeAttack); } }

    public Vector3 Direction { get => direction; set => direction = value; }
    public Vector2 Displacement { get => displacement; set => displacement = value; }
    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }

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
        sfx = GetComponent<AudioSource>();
        weaponMax = 1;
        #region Event Subs
        Enemy.onHit += MpRegain;
        Enemy.guardBreak += GuardBreak;
        AreaTransition.movePlayer += MovePlayerObject;
        Slam.slam += GroundSlamForce;
        GameController.onNewGame += SetDefault;
        GameController.respawn += RestoreHealth;
        GameController.respawn += ReturnToSpawn;
        onPlayerDeath += OnDead;
        Npc.dialogueUp += DialogueUp;
        Npc.dialogueDown += DialogueDown;
        UiManager.sealPlayerInput += SealInput;
        UiManager.sealPlayerInput += StopRunning;
        UiManager.unsealPlayerInput += UnsealInput;
        AIKryll.zend += BackToZend;
        GroundChecker.groundStatus += OnGrounded;
        GroundChecker.landed += SoundEffects;
        DoubleJump.doubleJump += SoundEffects;
        CinematicManager.unfade += SealInput;
        GameController.returnToLevelSelect += ReturnToSpawn;
        KillZone.respawn += ReturnToSpawn;
        SpellTag.triggerZaWarudo += ZaWarudo;
        JudgementCut.stop += ZaWarudo;
        DrawSword.hideSword += DrawSwordOut;
        Dash.dash += SoundEffects;
        Cast.setWeightBack += SetCastBack;
        EnemyHitBox.hit += OnHit;
        ReactionRange.dodged += ZaWarudo;
        UiManager.disablePlayer += DisableBody;
        UiManager.unsealPlayerInput += EnableBody;
        UiManager.angelSword += AngelSwordChose;
        UiManager.demonSword += DemonSwordChose;
        UiManager.bothSwords += BothSwordsChose;
        GroundSound.sendSound += GroundSoundManagement;
        EventTrigger.chooseSword += ChooseSword;
        WithdrawSword.withdraw += Withdraw;
        SceneDialogue.sealPlayerInput += SealInput;
        SceneDialogue.unsealPlayerInput += UnsealInput;
        SceneDialogue.sealPlayerInput += StopRunning;
        EventManager.demoRestart += SetDefault;
        Hurt.unseal += UnsealInput;
        //dpadLeft += AngelUp;
        dpadRight += DemonUp;
        dpadDown += Base;
        Interactable.sealJump += JumpSealer;
        Interactable.skullCollected += SkullMaskAdjuster;
        Interactable.bulbCollected += LightBulbAdjuster;
        LightBulbHolder.removeBulb += LightBulbAdjuster;
        Podium.skullUsed += SkullMaskAdjuster;
        FormSwitch.inviciblity += Endure;
        MovingStates.returnSpeed += ReturnSpeed;
        KillOtherLayers.weight += LayerControl;
        BaseBehavoirs.grounded += ZeroVelocity;
        PoisonLake.poisoned += TakeDamage;
        #region Item subs
        ItemData.mask += PowerUpp;
        #endregion
        #endregion
        ClothesSfx = zend.GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        battleMode = GetComponent<PlayerBattleSceneMovement>();
        headController = GetComponent<BasicHeadController>();
    }

    void Start() {
        //Stats.onStaminaChange+=StartCoroutine(StaminaRec());
        stats.Start();
        items.Start();
        Stats.onHealthChange += CheckPlayerHealth;
        demonLayer = anim.GetLayerIndex("DemonSwordLayer");
        angelLayer = anim.GetLayerIndex("AngelSwordLayer");
        guardLayer = anim.GetLayerIndex("GuardLayer");
        LegsLayer = anim.GetLayerIndex("RunningLayer");
        castLayer = anim.GetLayerIndex("MagicLayer");
        drawSwordLayer = anim.GetLayerIndex("DrawSwordLayer");

        grounded = anim.GetBool("Grounded");
        //InputSealed = true;
    }
    private void OnEnable() {
        if (onPlayerEnabled != null) {
            onPlayerEnabled();
        }
    }

    // Update is called once per frame
    private void Update() {
        Move();
        /*if (!pause && !InputSealed) {
            GetAllInput();

        }
        if ((inputSealed || pause) && Input.GetButtonDown("Circle")) {

            if (cancelPaused != null) {
                cancelPaused();
            }
        }
        if (!grounded && !CantDoubleJump) {

            if (Input.GetButtonDown("X")) {

                Player.GetPlayer().SecondJump = true;
                CantDoubleJump = true;
            }
        }
        
        OnPause();
        if (Input.GetKeyDown(KeyCode.I)) { SkillId = 1; }
        if (Input.GetKeyDown(KeyCode.P)) { Weapon = 4; }
        GetLStickPoistion();*/
    }
    #region Action Mapping
    private void OnCircle() {
        onCircle.Invoke();
    }
    private void OnJump() {
        if (grounded) {
            //SkillId = 10;
            anim.SetTrigger("Jump");
            //anim.SetLayerWeight(demonLayer, 0);
            //anim.SetLayerWeight(angelLayer, 0);
            Grounded = false;
            //StopCoroutine(WaitToFall());
            //GroundChecker.groundStatus -= OnGrounded;
            //StartCoroutine(ResetGroundCheck(0.3f));
            //StartCoroutine(WaitToFall());
            Rbody.AddForce(new Vector3(0, 333, 0), ForceMode.Impulse);
        }
    }
    private void OnSquare() { 
        
    }
    private void OnTriangle() { 
    
    }
    #endregion
    #region Helper Methods

    private void LegControl() {

        if (cmdInput == 0 && animations == 1 && !boosting) {
            if (playSound != null) {
                playSound(1);
            }
        }
        else {
            if (playSound != null) {
                playSound(0);
            }
        }
    }
    private void SealInput() => InputSealed = true;
    private void UnsealInput() => InputSealed = false;
    private void LayerControl(int val) {
        switch (style) {
            case 1:
                anim.SetLayerWeight(demonLayer, val);
                break;
            case 2:
                anim.SetLayerWeight(angelLayer, val);
                break;
        }
    }
    private void MovePlayerObject() {
        Attacking = false;
        //Nav.enabled = false;
        InputSealed = true;
        //RBody.isKinematic = false;
        Grounded = false;
    }
    /*private void CalculateMoveDirection() {
        Vector3 right;
        if (MovementBone != null) {

            right = MovementBone.TransformDirection(moveBoneRight);
        }
        else {
            right = transform.forward;
        }
        float dot = Vector3.Dot(right, displacement);
        anim.SetFloat("MoveDirectionX", dot);
    }
    private void DisplacementControl() {
        float x = Input.GetAxisRaw("Horizontal") * 0.05f * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * 0.05f * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn) {
            displacement = mainCam.GetComponent<ThreeDCamera>().XZOrientation.TransformDirection(displacement);
        }
    }*/
    private void SetPositionAndRotation(GameObject target) {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
    
    private void DialogueUp() => InputSealed = true;
    private void DialogueDown() => InputSealed = false;
    private void Move(float speed) {
        //transform.position += displacement *speed* Time.deltaTime;
        //rBody.AddForce(displacement*100);
        
        //rBody.velocity= displacement * speed;
        //Debug.Log(displacement);
        Rotate();
    }

    private void Rotate() {
        //if (Vector3.SqrMagnitude(displacement) > 0.01f&&!boosting) {
        //    transform.forward = displacement;
        //}
    }
    private void AirMove(float speed) {
        //
        /*if(!flying)
            RBody.AddForce(displacement/25, ForceMode.VelocityChange);
        else
            transform.position += displacement * speed *15* Time.deltaTime;
        */
        Rotate();
    }
    private void CheckPlayerHealth() {
        if (stats.HealthLeft <= 0 && !dead) { Dead = true; }
    }

    #endregion

    #region Movement 
    private void Move() {
        Direction = mainCam.transform.TransformDirection(new Vector3(displacement.x, 0, displacement.y).normalized);
        if (displacement.magnitude >= 0.1f) {
            
            Moving = true;
            
            direction.y = 0;
            Vector3 rot = Vector3.Normalize(Direction);
            rot.y = 0;
            qTo = Quaternion.LookRotation(direction);
            //MoveDirection = Quaternion.Euler(rot) * Camera.main.transform.forward;
            //transform.rotation = Quaternion.LookRotation(rot);
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotationSpeed);
        }
        else {
            Moving = false;
        }
    }
    private void OnMovement(InputValue value) {
        Displacement = value.Get<Vector2>();

    }
    #endregion
    private void GetAllInput() {
        //Archery();
        /*
        if (!jumping && grounded && !lockedOn&&!boosting) {
            MovementInput();
        }
        else if (cmdInput == 0&&!boosting) {
            CalculateRotation();

        }
        CalculateMoveDirection();
        if (Input.GetButtonDown("L1")) {

        }*/
        MenuNavi();
        if (L2.GetButtonDown()) {
            DemonFlame = true;
            Flying = true;
            rBody.useGravity = false;
        }
        if (L2.GetButtonUp()) {
            DemonFlame = false;
            Flying = false;
            rBody.useGravity = true;
        }

        if (Input.GetKey(KeyCode.U)&&style==2) {
            Charging = true;
        }
        if (!InHouse) {
            if (Input.GetButtonDown("R1")) {
                LongRangeAttack = true;
            }
            if (Input.GetButtonUp("R1")) {
                LongRangeAttack = false;
            }
            if (R2.GetButtonDown()&&grounded) {
                //PullUpTheBow();
                //Withdraw();
                //Debug.Log(bowUp);
                //BowUp = true;
                //AttackBow.SetActive(true);
                //if (archery != null) {
                //    archery(25);
                //}
                //StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 1, 0.2f, SetHeadWeight));
                //if (weak) {
                //    WoodenSword.SetActive(false);
                //}
                skillButton = true;
            }
            if (R2.GetButtonUp()) {
                //AttackBow.SetActive(false);
                //if (archery != null) {
                //    archery(0);
                //}
                //if (weak) {
                //    WoodenSword.SetActive(true);
                //}
                //BowUp = false;
                //StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 0, 0.2f, SetHeadWeight));
                //Debug.Log("false asf");
                skillButton = false;
            }
            if (!bowUp&&grounded) {

                LockOn();
                Block();
            }

            else {
                if (Input.GetButtonDown("L1")) {
                    CmdInput = 1;
                }
            }
            Skills();
            if (Input.GetButtonDown("Circle") && items.Items.Count > 0 && items.SelectedList == 1 && items.MainItem.Quantity > 0) {
                items.UseItem();

                anim.SetLayerWeight(castLayer, 1);
                CastItem = true;
                Debug.Log("Item used");
            }
            if (Input.GetButtonDown("Circle") && items.SelectedList == 0) {
                Attacking = false;
                Withdraw1 = true;
            }

            Sword();

            if (!jumpSeal && !lockedOn) {
                Jump();
            }
            if (!lockedOn && !weak) {
                //Dashu();

            }
        }
        if (jumping||demonFlame) {
            Rbody.useGravity = false;
        }
        else if(!jumping && !demonFlame) {
            Rbody.useGravity = true;
        }
        
        if (!grounded && cmdInput == 0) {
            //if (Jumping) {
            
        }
        
    }

    #region Time Stuff
    private void ZaWarudo() {
        Debug.Log("Za BITCH");
        if (!timeStopped) {
            Debug.Log("ZA HOE");
            zaWarudosRange.SetActive(true);
            Debug.Log("za warudo?");
            timeStopped = true;
            //StopTime = true;
            if (zaWarudo != null) {
                zaWarudo();
            }
            StartCoroutine(ResetTimeStop());
            StartCoroutine(UndoZaWarudo());

        }

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
    private void WeaponControl() {
        switch (weapon) {
            case 0:
                break;
            case 1:

                break;
        }
    }
    private void OnDeath() {
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
        GamePad.SetVibration(0, 0, 0);
        InputSealed = true;
        Withdraw();
    }
    private IEnumerator ResetTimeStop() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        timeStopped = false;
    }
    private void GetLStickPoistion() {

        float y = Input.GetAxis("Vertical");
        L = y;
    }
    

    private IEnumerator SetLayerWait() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        anim.SetLayerWeight(legsLayer, 1);

    }
    
    private void WeaponSwitch() {
        if (Input.GetAxis("DPad Right") > 0 && dPadRight.GetButtonDown()) {
            pressed = true;
            Debug.Log("right");
            if (style != 1) {
                if (dpadRight != null) {
                    dpadRight();
                }
            }

            if (weapon == weaponMax) {
                Weapon = 0;
            }
            else {
                Weapon++;
            }
        }
        if (Input.GetAxis("DPad Left") < 0 && !leftPressed) {
            leftPressed = true;
            Debug.Log("left");
            if (style != 2) {
                if (dpadLeft != null) {
                    dpadLeft();
                }
            }
            if (weapon == 0) {
                Weapon = weaponMax;
            }
            else {
                Weapon--;
            }
        }

        if (Input.GetAxis("DPad Left") >= 0) {
            leftPressed = false;
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


    private void MoveIt(float x, float y) {
        Vector3 offset = new Vector3(0, 0, 0);
        if (x != 0 || y != 0) {
            if (!Jumping && grounded) {
                //nav.enabled = true;
                Animations = 1;
                //anim.SetLayerWeight(6,0);
                if (Input.GetButtonDown("X")) {
                    //nav.enabled = false;
                    //MoveSpeed = 0.2f;
                }

                //MoveSpeed = 6;

            }
            else if (flying) {
                Animations = 2;
            }
            else {
                Animations = 0;

            }
            if (!grounded) {
                    AirMove(MoveSpeed);
            }else {
                    Move(MoveSpeed);
                }
        }
        else {

            Animations = 0;

        }
    }

    #region Coroutines
    private IEnumerator ResetGroundCheck(float reset) {
        YieldInstruction wait = new WaitForSeconds(reset);
        yield return wait;
        GroundChecker.groundStatus += OnGrounded;
    }
    private IEnumerator BackToZendWait() {

        yield return null;
        InputSealed = false;

    }
    
    private IEnumerator WaitToFall() {

        YieldInstruction wait = new WaitForSeconds(0.3f);
        yield return wait;
        Jumping = false;

    }
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
    private IEnumerator GuardCoroutine() {

        YieldInstruction wait = new WaitForSeconds(1f);
        yield return
        PerfectGuard = false;
        StopCoroutine(guardCoroutine);

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
            YieldInstruction wait = new WaitForSeconds(3f);//write variable to alter drain
            yield return wait;
            if (!runInEditMode) {
                stats.MPLeft--;
                if (stats.MPLeft == 0) {
                    Deform();
                }

            }
            
        }
    }

    #endregion
    #region Inputs
    private void Jump() {

        if (Input.GetButtonDown("X") && grounded) {
            SkillId = 10;
            Jumping = true;
            anim.SetLayerWeight(demonLayer, 0);
            anim.SetLayerWeight(angelLayer, 0);
            Grounded = false;
            StopCoroutine(WaitToFall());
            GroundChecker.groundStatus -= OnGrounded;
            StartCoroutine(ResetGroundCheck(0.3f));
            StartCoroutine(WaitToFall());
            Rbody.AddForce(new Vector3(0,333,0),ForceMode.Impulse);
        }
    }
    
    private void Dashu() {

        if (L2.GetButtonDown() && !SecondJump) {

            if (!Grounded) {
                Grounded = false;
            }

            Boosting = true;
            dash = true;
        }
        if (dash && Boosting) {
            StartCoroutine(Boost());
        }
    }

    private IEnumerator Boost() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        //RBody.useGravity = true;
        Boosting = false;
        //oveSpeed = 6;
    }

    private void Spells() {

        if (Input.GetButtonDown("X")) {
            L1X.Activate();
        }
        if (Input.GetButtonDown("Square")) {
            L1Square.Activate();
        }
        if (Input.GetButtonDown("Triangle")) {
            L1Triangle.Activate();
        }
        if (Input.GetButtonDown("Circle")) {
            L1Circle.Activate();
        }
    }
    private void WeaponManagement() {
        //DemonSword.SetActive(false);
        //demonFistLeft.SetActive(false);
        //demonFistRight.SetActive(false);
        //demonSwordBack.SetActive(true);
        //attackBow.SetActive(false);
        ////demonSwordBack.SetActive(true);
        if (!weak && attacking && grounded && !bowUp) {
            switch (style) {
                case 0:
                    //anim.SetLayerWeight(demonLayer, 1);
                    break;
                case 1:
                    anim.SetLayerWeight(demonLayer, 1);
                    break;
                case 2:
                    anim.SetLayerWeight(angelLayer, 1);
                    break;
            }
        }
    }
    private void Withdraw() {
        Withdraw1 = false;
        Attacking = false;
        DemonSword.SetActive(false);
        if (style == 0) {
            demonSwordBack.SetActive(true);
        }

        demonFistLeft.SetActive(false);
        demonFistRight.SetActive(false);
        //attackBow.SetActive(false);
        angelSword.SetActive(false);
        anim.SetLayerWeight(demonLayer, 0);
        anim.SetLayerWeight(angelLayer, 0);
        //BowUp = false;
    }
    private void Sword() {

        if (Input.GetButtonDown("Square") && !Attacking && !bowUp) {
            if (battleOn != null) {
                battleOn();
            }
            Attacking = true;
            CmdInput = 0;
            targeting = false;
            //BowDown();
            return;
        }
        if (attacking) {
            //WeaponManagement();


            if (Input.GetButtonDown("R1")) {
                Debug.Log("attacking is false");
                Withdraw1 = true;
                //LockedOn = false;
                return;
            }

            if (Input.GetButtonDown("Square")) {
                LightAttack = true;
            }
            if (Input.GetButtonDown("Triangle")) {
                StrongAttack = true;
            }
            if (Input.GetButtonUp("Square") && !skillIsActive && !boutaSpin) {
                LightAttack = false;
                CmdInput = 1;
            }
            if (Input.GetButtonUp("Triangle") && !skillIsActive) {
                CmdInput = 2;
                StrongAttack = false;
            }
        }
        else {
            Withdraw();
            Trail.SetActive(false);
            HitBox.SetActive(false);
            SkillId = 0;
            
            demonFistLeft.SetActive(false);
            demonFistRight.SetActive(false);
        }
    }
    private void Block() {
        if (Input.GetButtonDown("L1") && !skillIsActive) {
            PerfectGuard = true;
            guardCoroutine = StartCoroutine(GuardCoroutine());
        }
        if (Input.GetButton("L1") && !skillIsActive) {

            shieldBack.SetActive(false);
            if (cmdInput == 0) {
                Guard = true;
            }
            Animations = 0;
            //RBody.isKinematic = false;
            shield.SetActive(true);
            LockedOn = true;
            anim.SetLayerWeight(guardLayer, 1);

        }

        else {
            shield.SetActive(false);
            //Attacking = true;
            anim.SetLayerWeight(guardLayer, 0);
            Guard = false;
            shieldBack.SetActive(true);
        }
        if (Input.GetButtonUp("L1")) {
            LockedOn = false;
        }

    }
    private void Dodge() {
        GuardAnimations = 2;
    }
    private void MenuNavi() {
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            if (dpadUp != null) {
                dpadUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (style != 1) {
                if (dpadUp != null) {
                    dpadRight();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            if (style != 2) {
                if (dpadUp != null) {
                    dpadLeft();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (style != 0) {
                if (dpadUp != null) {
                    dpadDown();
                }
            }

        }
        if (Input.GetAxis("DPad Up") > 0 && dPadUp.GetButtonDown()) {
            pressed = true;
            Debug.Log("Up");
            if (dpadUp != null) {
                dpadUp();
            }
        }
        if ((Input.GetAxis("DPad Down") < 0 && !pressed)) {
            pressed = true;
            Debug.Log("down");
            if (dpadDown != null) {
                dpadDown();
            }
        }
        if (Input.GetAxis("DPad Down") >= 0) {
            pressed = false;
        }
        if (Input.GetAxis("DPad Right") > 0 && dPadRight.GetButtonDown()) {
            pressed = true;
            // Debug.Log("right");
            if (dpadRight != null) {
                dpadRight();
            }

        }
        if (Input.GetAxis("DPad Left") < 0 && !leftPressed) {
            leftPressed = true;
            //Debug.Log("left");
            if (dpadLeft != null) {
                dpadLeft();
            }
        }

        if (Input.GetAxis("DPad Left") >= 0) {
            leftPressed = false;
        }
    }
    private void Skills() {
        if (skillButton && Input.GetButtonDown("Triangle") && !skillIsActive) {
            if (triangle.SkillAssigned != null && stats.MPLeft >= triangle.MpRequired) {
                stats.MPLeft -= triangle.MpRequired;
                triangle.UseSkill();
                skillIsActive = true;
            }
        }

        if (skillButton && Input.GetButtonDown("Square") && !skillIsActive) {
            if (square.SkillAssigned != null && stats.MPLeft >= square.MpRequired) {
                stats.MPLeft -= square.MpRequired;
                square.UseSkill();
                skillIsActive = true;
            }
        }
        if (skillButton && Input.GetButtonDown("Circle") && !skillIsActive) {
            if (circle.SkillAssigned != null && stats.MPLeft >= circle.MpRequired) {
                stats.MPLeft -= circle.MpRequired;
                circle.UseSkill();
                skillIsActive = true;
                Guard = false;
            }
        }
        if (skillButton && Input.GetButtonDown("X") && !skillIsActive) {
            if (x.SkillAssigned != null && stats.MPLeft >= x.MpRequired) {
                stats.MPLeft -= x.MpRequired;
                x.UseSkill();
                skillIsActive = true;
            }
        }
    }
    #endregion
    private void LockOn() {

        if (Input.GetButton("R1") && !TeleportTriggered && zendSpace) {
            Time.timeScale = 0.1f;
            locked = true;
            if (Input.GetButtonDown("Triangle")) {
                TeleportTriggered = true;
                Cinemations = 51;
                Debug.Log("tf is good?");
            }
        }
        else {
            Time.timeScale = 1;
        }

        if (Input.GetButtonDown("L1")) {
            Animations = 0;
            Attacking = true;
            if (battleMode.Enemies.Count > 0) {
                if (findClosestEnemy != null)
                    findClosestEnemy();
            }
        }
        if (Input.GetButtonDown("L1")) {
            //Guard = true;

            LockedOn = true;
            if (playerIsLockedOn != null) {
                playerIsLockedOn();
            }
        }

        if (Input.GetButtonUp("L1")) {
            LockedOn = false;

            if (notAiming != null) {
                notAiming();

            }

        }
    }

    private void OnPause() {
        if (Input.GetButtonDown("Pause") && !Pause) {
            pauseMenu.SetActive(true);
            Pause = true;
            //UiManager.GetUiManager().Page = 0;
            return;
        }

        if (Pause) {
            if (Input.GetButtonDown("Pause")) {
                pauseMenu.SetActive(false);
                Pause = false;
                return;
            }
        }
    }

    #region Event handlers
    private void LightBulbAdjuster(int val) {
        Bulbs += val;
    }
    private void SkullMaskAdjuster(int val) {
        SkullMask += val;
        Debug.Log("number of skulls should go up");
    }
    private void JumpSealer(bool val) {
        JumpSeal = val;
    }
    private void Blank() {

        Attacking = false;

        halo.SetActive(false);
        lightning.SetActive(false);
        angelScabbard.SetActive(false);
        zendsLHorn.SetActive(false);
        zendsRHorn.SetActive(false);
        fireTrailR.SetActive(false);
        fireTrailL.SetActive(false);
        demonSwordBack.SetActive(false);
        demonScabbard.SetActive(false);
        WoodenSword.SetActive(false);
        Style = 0;
    }
    private void StyleSwitch() {

        if (!weak && hasBoth) {
            PowerUp = true;
            halo.SetActive(false);
            lightning.SetActive(false);
            angelScabbard.SetActive(false);
            zendsLHorn.SetActive(false);
            zendsRHorn.SetActive(false);
            fireTrailR.SetActive(false);
            fireTrailL.SetActive(false);
            demonSwordBack.SetActive(false);
            demonScabbard.SetActive(false);
            switch (style) {
                case 0:
                    Debug.Log("Base");
                    break;
                case 1:
                    AngelUp();
                    break;
                case 2:
                    DemonUp();
                    break;

            }
        }
    }
    private void StopRunning() {
        Animations = 0;
    }
    private void DemonSwordChose() {

        Attacking = false;
        Weak = false;
        weaponMin = 0;
        weaponMax = 1;
        DemonUp();
    }
    private void AngelSwordChose() {
        Attacking = false;
        Weak = false;
        weaponMin = 1;
        weaponMax = 2;
        AngelUp();
    }
    private void BothSwordsChose() {
        Attacking = false;
        Weak = false;
        weaponMax = 2;
        Style = 0;

        hasBoth = true;
    }
    private void AngelUp() {
        Blank();
        PowerUp = true;
        if (formChange != null) {
            formChange(1);
        }
        mpDrain = StartCoroutine(MpDrain());
        stats.Attack = 4;
        //Attacking = false;
        Weapon = 2;
        Style = 2;
        halo.SetActive(true);
        lightning.SetActive(true);
        angelScabbard.SetActive(true);
    }
    private void DemonUp() {
        Blank();
        PowerUp = true;
        if (formChange != null) {
            formChange(1);
        }
       mpDrain = StartCoroutine(MpDrain());
        //Attacking = false;
        stats.Attack = 5;
        Weapon = 0;
        Style = 1;
        zendsLHorn.SetActive(true);
        zendsRHorn.SetActive(true);
        fireTrailR.SetActive(true);
        fireTrailL.SetActive(true);
        //demonSwordBack.SetActive(true);
        //demonScabbard.SetActive(true);
    }
    private void Base() {
        //wod
        StopCoroutine(mpDrain);
        if (formChange != null) {
            formChange(0);
        }
        stats.Attack = 3;
        Blank();
        //demonSwordBack.SetActive(true);
        //demonScabbard.SetActive(true);
        //WoodenSword.SetActive(true);
    }
    private void ChooseSword() {

        WoodenSword.SetActive(false);
         
    }
    private void SetCastBack() {
        anim.SetLayerWeight(castLayer, 0);
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
        if (!hit&&!endure) {
            Hit = true;
            SealInput();
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
    
    private IEnumerator WaitToLand() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        InputSealed = false;
    }
    private void ReturnToSpawn() {

        MovePlayerObject();
        transform.position = GameController.GetGameController().Spawn.transform.position;
        UnsealInput();
        //StartCoroutine(WaitToLand());
    }
    private void GroundSlamForce(float force) {
        Rbody.mass = force;
    }
    private void OnGrounded(bool val) {
        Grounded = val;
        //RBody.isKinematic = val;
    }

    private void OnDead() {

    }
    private void BackToZend() {
        //main.SetActive(true);
        StartCoroutine(BackToZendWait());

    }
    private void Respawn() {
        transform.position = GameController.GetGameController().Spawn.transform.position;
    }
    private void TakeDamage(int damage) {
        stats.HealthLeft -= damage;
        GamePad.SetVibration(0, 0.2f, 0.2f);
    }
    private void SetDefault() {

        PostProcessorManager.GetProcessorManager().Default();
        Attacking = false;
        CmdInput = 0;
        //MoveSpeed = 6;
        LockedOn = false;
        stats.Start();
        //ReturnToSpawn();
        mask.SetActive(false);
        GameObject aura = transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(aura);
        //GetComponentInChildren<SkinnedMeshRenderer>().material = normal;
        battleMode.Enemies.Clear();
        Dead = false;
        Money = 1000;
        //eak = true;
        zendsLHorn.SetActive(false);
        zendsRHorn.SetActive(false);
        fireTrailR.SetActive(false);
        fireTrailL.SetActive(false);
        //demonSwordBack.SetActive(false);
        //demonScabbard.SetActive(false);
        halo.SetActive(false);
        lightning.SetActive(false);
        angelScabbard.SetActive(false);
        Style = 0;
        //woodenSword.SetActive(false);
    }
    private void RestoreHealth() {
        Dead = false;
        stats.HealthLeft = stats.Health;
    }
    
    private void Deform() {
        //PostProcessorManager.GetProcessorManager().Default();
        PoweredUp = false;

        Flying = false;
        rBody.useGravity = true;
        Instantiate(swordDSpawn, transform);
        Base();


        StopCoroutine(mpDrain);

    }
    private void PowerUpp() {
        mpDrain = StartCoroutine(MpDrain());
        //StopCoroutine(staminaRec);
        //FireTrail.SetActive(true);
        mask.SetActive(true);
        PoweredUp = true;
        PowerUp = true;

    }
    private void ZeroVelocity() {
        rBody.velocity = new Vector3(0,0,0);
    }
    private void ReturnSpeed(float val) {
        StartCoroutine(Lowkey(val));
    }
    private IEnumerator Lowkey(float val) {
        YieldInstruction wait = new WaitForSeconds(0.3f);
        yield return wait;
        MoveSpeed = val;
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

    private void OnTriggerEnter(Collider other) {
    
    if (other.CompareTag("Item")) {
    
        stats.HealthLeft += 2;
            Destroy(other.gameObject);
    
    }
    
}
}
