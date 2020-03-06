using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using XInputDotNetPure;
#pragma warning disable 0649
public class Player : MonoBehaviour {
    //private bool usingController;
    [Header("Movement")]
    private bool moving;
    //public float speed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float burstForce;
    private int money;
    private bool inputSealed;
    #region Attacking
    [Space]
    [Header("Attacking")]
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject fistHitBox;
    private bool attacking;
    private bool skillButton;
    private bool lockedOn;
    [SerializeField] private GameObject swordSpawn;
    [SerializeField] private GameObject swordDSpawn;

    private bool skillIsActive;
    [SerializeField] GameObject aimmingPoint;
    private int weapon;
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
    [SerializeField] private GameObject demonFistLeft;
    [SerializeField] private GameObject demonFistRight;
    [SerializeField] private GameObject guitar;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject attackBow;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject shield;
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

    private bool rockOut;
    private bool pickUp;
    private bool wall;
    private bool climbing;
    private bool chopping;
    private bool grounded;
    private bool transforming;
    private bool wallMoving;
    private bool leftDash;
    private bool rightDash;
    private bool guard;
    private bool hit;
    private bool dead;
    private int direction;
    private bool stop;
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
    [Space]
    [Header("References To Things on Zend")]
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
    [Space]
    [Header("HitBoxes")]
    [SerializeField] private GameObject AoeHitbox;
    [SerializeField] private GameObject forwardHitbox;

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
    private NavMeshAgent nav;
    private PlayerBattleSceneMovement battleMode;
    private Animator anim;
    private Vector3 displacement;//world space 


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

    [SerializeField] private GameObject jumpPoint;
    private bool doubleJump;
    private bool boosting;
    private bool teleportTriggered;
    private bool timeStopped;
    private bool stopTime;
    private bool pressed;
    private float weaponAmount = 1;
    private bool locked;

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
    public static event UnityAction weaponSwitch;
    public static event UnityAction attackModeUp;
    public static event UnityAction findClosestEnemy;
    public static event UnityAction unlocked;
    #endregion
    //Optimize these to use only one Animation parameter in 9/14
    #region Getters and Setters
    public bool RockOut { get => rockOut; set { rockOut = value; anim.SetBool("RockOut", rockOut); } }
    public bool PickUp1 { get => pickUp; set { pickUp = value; anim.SetBool("PickUp", pickUp); } }
    public bool Wall { get => wall; set => wall = value; }
    public bool Climbing1 { get => climbing; set { climbing = value; anim.SetBool("Climbing", climbing); } }
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); if (grounded) { RBody.isKinematic = true; nav.enabled = true; SecondJump = false; } } }

    public bool WallMoving { get => wallMoving; set { wallMoving = value; anim.SetBool("WallMoving", wallMoving); } }
    public bool LeftDash { get => leftDash; set { leftDash = value; anim.SetBool("LeftDash", leftDash); } }
    public bool RightDash { get => rightDash; set { rightDash = value; anim.SetBool("RightDash", rightDash); } }
    public bool Guard { get => guard; set { guard = value; if (value) Moving = false; shield.SetActive(value); anim.SetBool("Guard", guard); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); if (attackModeUp != null) { attackModeUp(); } } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }

    public byte Timer { get => timer; set => timer = value; }
    public GameObject Body { get => body; set => body = value; }
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); if (hit) { hitDefuse = StartCoroutine(HitDefuse()); } } }
    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); if (dead) { OnDeath(); } else { Debug.Log("Its ALIVEEEE"); } } }
    public int Direction { get => direction; set { direction = value; anim.SetInteger("Direction", direction); } }
    //public Vector3 Delta { get => delta; set => delta = value; }

    public bool Stop { get => stop; set => stop = value; }
    public bool Pause { get => pause; set { pause = value; if (pause) { Time.timeScale = 0; } else { Time.timeScale = 1; } } }
    public bool Loaded { get => loaded; set { loaded = value; Nav.enabled = value; } }

    public GameObject FireCaster { get => fireCaster; set => fireCaster = value; }

    public PlayerBattleSceneMovement BattleMode { get => battleMode; set => battleMode = value; }
    public GameObject DemonSword { get => demonSword; set => demonSword = value; }
    public GameObject HitBox { get => hitBox; set { hitBox = value; } }

    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("Skill ID", skillId); if (skillId == 0) { SkillIsActive = false; } } }
    public Rigidbody RBody { get => rBody; set => rBody = value; }
    public NavMeshAgent Nav { get => nav; set { nav = value; } }
    public Animator Anim { get => anim; set => anim = value; }
    public bool Chopping { get => chopping; set { chopping = value; anim.SetBool("Chopping", chopping); } }

    public GameObject Axe { get => axe; set => axe = value; }
    public bool PerfectGuard { get => perfectGuard; set => perfectGuard = value; }
    public GameObject ForwardHitbox { get => forwardHitbox; set => forwardHitbox = value; }
    public GameObject FireTrail { get => fireTrail; set => fireTrail = value; }
    public GameObject AoeHitbox1 { get => AoeHitbox; set => AoeHitbox = value; }
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", animations); if (animations == 1) { clothesSfx.volume = 0.5f; } else { clothesSfx.time = 0; clothesSfx.volume = 0; } } }

    public bool LockedOn { get => lockedOn; set { lockedOn = value; if (!LockedOn) { if (unlocked != null) unlocked(); Direction = 0; } } }

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

    public GameObject DevilFoot { get => devilFoot; set => devilFoot = value; }
    public GameObject LeftHand { get => leftHand; set => leftHand = value; }
    public GameObject RightHand { get => rightHand; set => rightHand = value; }
    public int Cinemations { get => cinemations; set { cinemations = value; anim.SetInteger("Cinemaitions", cinemations); } }

    public bool TeleportTriggered { get => teleportTriggered; set => teleportTriggered = value; }
    //I set the clamp on the weapon thing to 0 cuz demon fist werent ready for testing yet.
    public int Weapon { get => weapon; set { weapon = Mathf.Clamp(value, 0, 0); anim.SetInteger("Weapon", weapon); if (weaponSwitch != null) { weaponSwitch(); } } }

    public GameObject FistHitBox { get => fistHitBox; set => fistHitBox = value; }
    public bool StopTime { get => stopTime; set { stopTime = value; anim.SetBool("TimeStop", stopTime); } }

    public GameObject Trail { get => trail; set => trail = value; }
    public bool SecondJump { get => doubleJump; set => doubleJump = value; }
    public bool InHouse { get => inHouse; set { inHouse = value; if (inHouse) { DemonSword.SetActive(false); demonSwordBack.SetActive(true); } } }

    public GameObject BattleCamTarget { get => battleCamTarget; set => battleCamTarget = value; }
    public float BurstForce { get => burstForce; set => burstForce = value; }
    public GameObject CenterPoint { get => centerPoint; set => centerPoint = value; }

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
        #region Event Subs
        Enemy.onHit += MpRegain;
        PortalManager.backToBase += BackToBase;
        AreaTransition.movePlayer += MovePlayerObject;
        Slam.slam += GroundSlamForce;
        Objective.rewardPlayer += RewardPlayer;
        GameController.onNewGame += SetDefault;
        onPlayerDeath += OnDead;
        Npc.dialogueUp += DialogueUp;
        Npc.dialogueDown += DialogueDown;
        UiManager.bedTime += Sleep;
        UiManager.outaBed += OutaBed;
        AIKryll.zend += BackToZend;
        GroundChecker.groundStatus += OnGrounded;
        UiManager.sealPlayerInput += SealInput;
        CinematicManager.unfade += SealInput;
        CinematicManager.gameStart += UnsealInput;
        UiManager.unsealPlayerInput += UnsealInput;
        KillZone.respawn += ReturnToSpawn;
        SpellTag.triggerZaWarudo += ZaWarudo;
        FreeFallZend.landed += BackToBase;
        #endregion
        clothesSfx = zend.GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        battleMode = GetComponent<PlayerBattleSceneMovement>();
        headController = GetComponent<BasicHeadController>();


    }

    void Start() {
        //Stats.onStaminaChange+=StartCoroutine(StaminaRec());
        stats.Start();
        items.Start();
        Stats.onHealthChange += CheckPlayerHealth;

        grounded = anim.GetBool("Grounded");

    }
    private void OnEnable() {
        if (onPlayerEnabled != null) {
            onPlayerEnabled();
        }
        //StartCoroutine(WaitForGame());
        //staminaRec = StartCoroutine(StaminaRec());
    }
    // Update is called once per frame
    void Update() {
        if (!pause && !InputSealed) {
            GetAllInput();

        }
        if ((inputSealed || pause) && Input.GetButtonDown("Circle")) {

            if (cancelPaused != null) {
                cancelPaused();
            }
        }
        if (!grounded && !jumping) {
            //transform.position -= new Vector3(0, 7.2f, 0) * Time.deltaTime;

        }
        WhileSleep();
        OnPause();
        //if (Input.GetKeyDown(KeyCode.P)) { SkillId = 5; }
    }
    #region Helper Methods
    private void SealInput() => InputSealed = true;
    private void UnsealInput() => InputSealed = false;
    private void MovePlayerObject() {
        Attacking = false;
        Nav.enabled = false;
        InputSealed = true;
        RBody.isKinematic = false;
        Grounded = false;
    }
    private void CalculateMoveDirection() {
        Vector3 right;
        if (movementBone != null) {

            right = movementBone.TransformDirection(moveBoneRight);
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
    }
    private void SetPositionAndRotation(GameObject target) {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
    private void RewardPlayer(int rewardMoney, int rewardExp, Items rewardItem) {
        StartCoroutine(RewardCoroutine(rewardMoney, rewardExp, rewardItem));
    }
    private void DialogueUp() => InputSealed = true;
    private void DialogueDown() => InputSealed = false;
    public void Move(float speed) {
        transform.position += displacement * speed * Time.deltaTime;
        Rotate();
    }
    private void Rotate() {
        if (Vector3.SqrMagnitude(displacement) > 0.01f) {
            transform.forward = displacement;
        }
    }
    private void CheckPlayerHealth() {
        if (stats.HealthLeft <= 0) { Dead = true; }
    }
    #endregion
    #region Event handlers
    private void MpRegain() {
        stats.MPLeft += 2;
    }
    private void BackToBase(Vector3 destination, bool houseOrNot) {
        InHouse = houseOrNot;
        Attacking = false;
        MovePlayerObject();
        transform.position = destination;
        StartCoroutine(WaitToLand());
    }
    private IEnumerator WaitToLand() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        InputSealed = false;
    }
    private void ReturnToSpawn() {

        MovePlayerObject();
        transform.position = GameController.GetGameController().Spawn.transform.position;
        StartCoroutine(WaitToLand());
    }
    private void GroundSlamForce(float force) {
        RBody.mass = force;
    }
    private void OnGrounded(bool val) {
        Grounded = val;
        RBody.isKinematic = val;
    }

    private void OnDead() {
        UiManager.GetUiManager().DefeatedWindow();
        //GetComponentInChildren<SkinnedMeshRenderer>().material = fader;
        //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 1);

    }
    private void Sleep() {
        Body.SetActive(false);
        sleep = true;
        stats.HealthLeft = stats.Health;
        InputSealed = true;
    }
    private void BackToZend() {
        //main.SetActive(true);
        StartCoroutine(BackToZendWait());

    }
    private void Respawn() {
        transform.position = GameController.GetGameController().Spawn.transform.position;
    }
    private void SetDefault() {
        PostProcessorManager.GetProcessorManager().Default();
        Attacking = false;
        CmdInput = 0;
        MoveSpeed = 6;
        LockedOn = false;
        stats.Start();

        mask.SetActive(false);
        GameObject aura = transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(aura);
        //GetComponentInChildren<SkinnedMeshRenderer>().material = normal;
        battleMode.Enemies.Clear();
        Dead = false;
        Money = 1000;

    }
    private void Kryll() {
        InputSealed = true;

        if (kryll != null) {
            kryll();
        }
    }
    private void Deform() {
        PostProcessorManager.GetProcessorManager().Default();
        PoweredUp = false;
        stats.Attack /= 2;
        stats.Defense /= 2;
        GameObject aura = transform.GetChild(transform.childCount - 1).gameObject;
        Instantiate(swordDSpawn, transform);
        FireTrail.SetActive(false);
        mask.SetActive(false);
        Destroy(aura);
        StopCoroutine(mpDrain);
        staminaRec = StartCoroutine(StaminaRec());
    }
    private void PowerDown() {
        mpDrain = StartCoroutine(MpDrain());
        StopCoroutine(staminaRec);
        FireTrail.SetActive(true);
        mask.SetActive(true);
        PoweredUp = true;
        PowerUp = true;

    }

    #endregion


    private void GetAllInput() {
        //Archery();

        if (!jumping&&grounded && !guard && !lockedOn && MoveSpeed > 0) {
            MovementInput();
        }
        else if (cmdInput == 0) {
            CalculateRotation();

        }
        CalculateMoveDirection();
        if (Input.GetButton("L1")) {

            Spells();
        }
        if (Input.GetButtonDown("L1")) {

        }


        if (!InHouse) {
            WeaponSwitch();

            LockOn();
            Skills();

            if (attacking && R2.GetButton()) {
                skillButton = true;
            }
            else
                skillButton = false;
            Sword();

            if (!jumpSeal) {
                Jump();
            }
            if (!lockedOn) {
                Dash();

            }
            
            //DoubleJump();

        }

        if (!grounded && cmdInput == 0) {
            //if (Jumping) {
            AirMovementInput();

            //}
            if (!jumping && !boosting && !locked) {
                transform.position -= new Vector3(0, 0.1f, 0);
            }
            if (Input.GetButton("X") && !jumping && !boosting && !SecondJump) {
                RBody.drag = 25;

            }

        }
        else {

            RBody.drag = 0;
        }

        //Inventory();
        //Guitar();



        /*if (dPadUp.GetButtonDown()) {
            Animations = 99;

        }*/
        //if (Input.GetKeyDown(KeyCode.P)) { stats.AddExp(1000); }
    }
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

    }
    private void OnDeath() {
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
        GamePad.SetVibration(0, 0, 0);
        InputSealed = true;
    }
    private void AirMovementInput() {
        float x = Input.GetAxisRaw("Horizontal") * 0.005f * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * 0.005f * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn) {
            displacement = mainCam.GetComponent<ThreeDCamera>().XZOrientation.TransformDirection(displacement);
        }
        MoveIt(x, y);
    }
    private IEnumerator ResetTimeStop() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        timeStopped = false;
    }
    private void CalculateRotation() {


        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn) {
            displacement = mainCam.GetComponent<ThreeDCamera>().XZOrientation.TransformDirection(displacement);
        }
        Rotate();

    }
    private void MovementInput() {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn) {
            displacement = mainCam.GetComponent<ThreeDCamera>().XZOrientation.TransformDirection(displacement);
        }
        if (Input.GetButtonDown("L3")) {
            //Grounded = true;

            //Kryll();
        }
        /*if (Input.GetButtonDown("R3") && !transforming && stats.MPLeft >= 1) {
            if (poweredUp) {
                Deform();
                //stats.MPLeft--;
            }
            else {
                PowerDown();

            }
        }
        if (poweredUp && stats.MPLeft == 0) {
            Deform();
        }*/

        //WeaponSwitch();

        MoveIt(x, y);


    }
    private void WhileSleep() {
        if (sleep) {
            if (Input.GetButtonDown("Circle")) { 
                if (notSleeping != null) {
                    notSleeping();
                }
            }
        }
    }
    private void OutaBed() {
        Debug.Log("no longer sleeppppp");
        Sleeping = false;
        InputSealed = false;
        body.SetActive(true);
        SetPositionAndRotation(outaBed);
    }
    private void WeaponSwitch() {
        if (Input.GetAxis("DPad Up") > 0 && dPadUp.GetButtonDown()) {
            pressed = true;
            Debug.Log("flicked with no flicker");
            Weapon++;
        }
        if ((Input.GetAxis("DPad Down") < 0 && !pressed)) {
            pressed = true;
            Weapon--;
        }
        if (Input.GetAxis("DPad Down") >= 0) {
            pressed = false;
        }
        /*if (Input.GetButtonDown("R3")) {
            Debug.Log("R3 flicked with no flicker");
            Weapon++;
        }
        if (Input.GetButtonDown("L3")) {
            Weapon--;
        }*/
    }
    #region Unused Methods
    private void Archery() {

        if (bowUp) {
            if (Input.GetButton("Square")) {


            }
            if (Input.GetButtonUp("Square")) {


            }
            if (R2.GetButton()) {
                CmdInput = 5;
                MoveSpeed = 2;

                if (aiming != null) {
                    aiming();
                }

            }
            if (R2.GetButtonUp()) {
                CmdInput = 6;
                MoveSpeed = 6;

            }


        }

        if (L2.GetButtonDown()) {
            Attacking = false;
            //targeting = true;
            BowUp = true;
            AttackBow.SetActive(true);


            StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 1, 0.2f, SetHeadWeight));
        }
        if (L2.GetButton()) {
            targeting = true;



        }

        if (L2.GetButtonUp()) {


            targeting = false;

        }
        if (R2.GetButtonDown()) {

        }

        if (!bowUp) {
            StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 0, 0.2f, SetHeadWeight));///GOOD CODE!!!!!

        }        //anim.


        //anim.SetLookAtPosition();
        //anim.SetLookAtWeight(testWeight, testBodyWeight, testHeadWeight, testEyesWeight, testClampWeight);
        headBone.transform.LookAt(ThreeDCamera.Retical.transform.position);

        if (targeting) {
            //anim.GetBoneTransform(HumanBodyBones.Spine).transform.LookAt(ThreeDCamera.Retical.position);



        }



    }
    private void BowDown() {
        if (notAiming != null) {
            notAiming();

        }

        AttackBow.SetActive(false);
        BowUp = false;

    }
    private void Guitar() {
        if (Input.GetKey(KeyCode.R) && items.HasItem(2)) {

            RockOut = true;
        }
    }
    private void SetHeadWeight(float result) {
        headController.Weight = result;
    }
    #endregion


    private void MoveIt(float x, float y) {
        Vector3 offset = new Vector3(0, 0, 0);
        if (x != 0 || y != 0) {
            if (!Jumping && grounded) {
                nav.enabled = true;
                Animations = 1;
                if (Input.GetButtonDown("X")) {
                    nav.enabled = false;
                    //MoveSpeed = 0.2f;
                }
                MoveSpeed = 6;

            }
            else {
                Animations = 0;

            }
            if (!grounded) {
                if (Jumping) {
                    MoveSpeed = 13;
                }
                else {
                    MoveSpeed = 3;

                }

            }
            Move(MoveSpeed);

            if (bowUp && !targeting) {

                StartCoroutine(StopTargeting());

            }
            transform.position += offset;

            if (attacking && Input.GetButtonDown("Square")) {

            }
        }
        else {

            Animations = 0;
            nav.enabled = false;
            //Moving = false;
        }
    }

    #region Coroutines
    private IEnumerator ResetGroundCheck(float reset) {
        YieldInstruction wait = new WaitForSeconds(reset);
        yield return wait;
        global::GroundChecker.groundStatus += OnGrounded;
    }
    private IEnumerator BackToZendWait() {

        yield return null;
        InputSealed = false;

    }
    private IEnumerator RewardCoroutine(int rewardMoney, int rewardExp, Items rewardItem) {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        Money += rewardMoney;
        items.AddItem(rewardItem.data);
        stats.AddExp(rewardExp);

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
    private IEnumerator HitDefuse() {
        YieldInstruction wait = new WaitForSeconds(0.3f);
        yield return
        Hit = false;
        StopCoroutine(hitDefuse);
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
    private IEnumerator StopTargeting() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        BowDown();


    }
    private IEnumerator MpDrain() {
        while (isActiveAndEnabled) {
            YieldInstruction wait = new WaitForSeconds(1.5f);
            yield return wait;
            stats.MPLeft--;
        }

    }

    #endregion
    #region Inputs
    private void Jump() {

        if (Input.GetButtonDown("X") && grounded) {
            SkillId = 10;
            if (AIKryll.disableCollider != null) {
                AIKryll.disableCollider();
            }
            //transform.position +=new Vector3(0, 5, 0)  * Time.deltaTime;

            Debug.Log("ORA");
            Jumping = true;
            nav.enabled = false;
            RBody.isKinematic = false;
            Grounded = false;
            //transform.position=Vector3.MoveTowards(transform.position, jumpPoint.transform.position, 200f * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, jumpPoint.transform.position, 200f * Time.deltaTime);
            //
            StopCoroutine(WaitToFall());
            GroundChecker.groundStatus -= OnGrounded;
            StartCoroutine(ResetGroundCheck(0.3f));
            StartCoroutine(WaitToFall());

            //MoveIt(0, 15);
        }

        if (Jumping) {
            //transform.position += new Vector3(0, 6, 0) * Time.deltaTime;
            //RBody.AddForce(new Vector3(0, 6, 0), ForceMode.VelocityChange);
            transform.position = Vector3.MoveTowards(transform.position, jumpPoint.transform.position, jumpForce * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, jumpPoint.transform.position, 19f * Time.deltaTime);
        }


    }
    private void Dash() {
        
        if (L2.GetButtonDown() && !SecondJump) {
             
             
            if (!Grounded) {
                Grounded = false;
                //transform.position = Vector3.Lerp(transform.position, jumpPoint.transform.position, 150f * Time.deltaTime);
                //*Time.deltaTime
                nav.enabled = false;
                //RBody.isKinematic = false;
                

                //Boosting = true;
                //dash = true;
                //doubleJump = true;
                //Debug.Log("DoubleJump Triggered");
                
            }//StopCoroutine(WaitToFall());
             //
             //   GroundChecker.groundStatus -= OnGrounded;
             //   StartCoroutine(ResetGroundCheck(0.5f));
             //   StartCoroutine(WaitToFall());
            
                Boosting = true;
                dash = true;
                RBody.isKinematic = false;
            
        }
        if (dash && Boosting) {
           
            if (Grounded) {
                BurstForce = 15;
            }
            else {
                BurstForce = 30;
                CmdInput = 0;
            RBody.isKinematic = false;
            RBody.AddForce(transform.forward * BurstForce, ForceMode.VelocityChange);
            if (RBody.isKinematic) {
                Debug.Log("wtf is good?");
            }
            }
            Debug.Log("Boosting");
            
            //MoveSpeed = 13;
            StartCoroutine(Boost());
        }
    }
    private void DoubleJump() {
        if (!grounded && !jumping && Input.GetButtonDown("X")) {
            Debug.Log("Double jump");
            nav.enabled = false;
            SecondJump = true;
            SkillId = 10;
            RBody.useGravity = false;
            StopCoroutine(WaitToFall());
            StartCoroutine(ResetGroundCheck(0.5f));
            StartCoroutine(WaitToFall());
        }
        if (SecondJump) {
            RBody.AddForce(transform.up * 30, ForceMode.VelocityChange);
            StartCoroutine(StopDoubleJump());
        }
    }
    private IEnumerator Boost() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        RBody.useGravity = true;
        Boosting = false;
        MoveSpeed = 6;
    }
    private IEnumerator StopDoubleJump() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        SecondJump = false;
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
        DemonSword.SetActive(false);
        demonFistLeft.SetActive(false);
        demonFistRight.SetActive(false);
        attackBow.SetActive(false);
        demonSwordBack.SetActive(true);
        switch (weapon) {
            case 0:
                demonSwordBack.SetActive(false);
                DemonSword.SetActive(true);
                Trail.SetActive(true);
                break;
            case 1:
                demonFistLeft.SetActive(true);
                demonFistRight.SetActive(true);
                break;
            case 2:
                attackBow.SetActive(true);
                break;
        }
    }
    private void Sword() {

        if (Input.GetButtonDown("Square") && !Attacking) {
            if (battleOn != null) {
                battleOn();
            }
            Attacking = true;

            CmdInput = 0;
            MoveSpeed = 6;
            targeting = false;
            BowDown();
            return;
        }
        if (Attacking) {
            WeaponManagement();


            if (Input.GetButtonDown("Circle") && !skillIsActive) {
                PerfectGuard = true;
                guardCoroutine = StartCoroutine(GuardCoroutine());
            }
            if (Input.GetButton("Circle") && !skillIsActive) {
                Guard = true;
                Animations = 0;
                RBody.isKinematic = false;
            }
            else {
                Guard = false;
            }

            if (Input.GetButtonDown("L1")) {
                Debug.Log("attacking is false");
                Attacking = false;
                LockedOn = false;
                return;
            }



            if (Input.GetButtonDown("Square") && !skillIsActive) {
                CmdInput = 1;
                //GamePad.SetVibration(0,0.5f,0.5f);
                //StartCoroutine(KillVibration());
            }
            if (Input.GetButtonDown("Triangle") && !skillIsActive) {
                CmdInput = 2;
            }
        }
        else {

            Trail.SetActive(false);
            HitBox.SetActive(false);
            SkillId = 0;
            DemonSword.SetActive(false);
            demonSwordBack.SetActive(true);
            demonFistLeft.SetActive(false);
            demonFistRight.SetActive(false);
            attackBow.SetActive(false);
            
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
        /*if (stats.MPLeft >= 2 && !bowUp && !skillIsActive && Input.GetButtonDown("X") )
        {

            SkillId = 10;
            //stats.MPLeft -= 2;
            skillIsActive = true;

        }*/
    }
    #endregion
    private void LockOn() {
        if (BattleMode.Enemies.Count > 0) {
            if (Input.GetButton("R1") && !TeleportTriggered && zendSpace) {
                //StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 1, 0.2f, SetHeadWeight));
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

                
                //StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 0, 0.2f, SetHeadWeight));
            }

            if (Input.GetButtonDown("R1")) {
                Animations = 0;
                Attacking = true;
                //if (lockOn != null) {
                //    lockOn();
                //}
                if (findClosestEnemy != null)
                    findClosestEnemy();
            }
            if (Input.GetButton("R1")) {
                //Guard = true;
                Debug.Log("fuck this code");
                LockedOn = true;
                if (playerIsLockedOn != null) {
                    playerIsLockedOn();
                }
                
                //StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 1, 0.2f, SetHeadWeight));

            }
            else {
                
                //StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 0, 0.2f, SetHeadWeight));
                LockedOn = false;
                //Guard = false;
            }
        }
        else {

            //if (notAiming != null)
            //{
            // notAiming();

            //}
            LockedOn = false;
        }
        if (Input.GetButtonUp("R1")) {
            //if (notAiming != null) {
            //        notAiming();
            //
            //    }
            // if (notAiming != null)
            //{
            // notAiming();

            //}

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
            /*
            if (Input.GetButtonDown("R1"))
            {
                UiManager.GetUiManager().Page++;

            }
            if (Input.GetButtonDown("L1"))
            {
                UiManager.GetUiManager().Page--;

            }*/
        }

    }
    public void PickUp(Items other) {
        PickUp1 = true;
        Wall = false;
        Timer = 5;
        items.AddItem(other.GetComponent<Items>().data);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Item")) {
            items.AddItem(other.GetComponent<Items>().data);
            other.gameObject.SetActive(false);
            Destroy(other);
        }
    }
}
