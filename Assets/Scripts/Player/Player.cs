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
    private bool weak;
    private int style;

    //public float speed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float burstForce;
    [SerializeField] private float rotationSpeed;
    Vector3 directionV;
    Vector2 displacementV;
    Quaternion qTo;
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
    private bool attackState;
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

    [SerializeField] private Transform movementBone;
    [SerializeField] private Transform headBone;
    //[SerializeField] private Vector3 moveBoneForward = new Vector3(0, 0, 1);
    [SerializeField] private Vector3 moveBoneRight = new Vector3(1, 0, 0);

    private bool grounded;
    private bool blocking;
    private bool transforming;

    private bool leftDash;
    private bool rightDash;
    private bool guard;
    private bool hit;
    private bool dead;
    private int direction;

    private bool charging;

    private int skillId;
    private bool powerUp;
    private bool sleep;
    private int cmdInput;
    private int animations;
    private bool bowUp;
    private bool poweredUp;
    private bool jumping;
    private int cinemations;
    private bool inHouse;
    private int combatAnimations;
    private int guardAnimations;
    private bool doubleJump;
    private bool spinAttack;
    private float speedInc;

    private int demonLayer;
    private int angelLayer;
    private int legsLayer;
    private int guardLayer;
    private int castLayer;
    private int shootLayer;
    private int drawSwordLayer;
    private int airCombo;
    #endregion
    #region Random stuff
    [Space]
    [Header("OtherFunctions")]
    private Rigidbody rBody;
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
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject arrowPoint;
    [SerializeField] private GameObject farHitPoint;

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
    private Vector3 displacement;//world space 
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

    #endregion
    //Optimize these to use only one Animation parameter in 9/14
    #region Getters and Setters
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); WeaponManagement(); if (grounded) { /*RBody.isKinematic = true; /*nav.enabled = true;*/ SecondJump = false; CantDoubleJump = true; } } }
    public bool LeftDash { get => leftDash; set { leftDash = value; anim.SetBool("LeftDash", leftDash); } }
    public bool RightDash { get => rightDash; set { rightDash = value; anim.SetBool("RightDash", rightDash); } }
    public bool Guard { get => guard; set { guard = value; anim.SetBool("Guard", guard); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); WeaponManagement(); if (attackModeUp != null) { attackModeUp(); } } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public GameObject Body { get => body; set => body = value; }
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); } }
    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); if (dead) { OnDeath(); } else { } } }
    public int Direction { get => direction; set { direction = value; anim.SetInteger("Direction", direction); } }
    public bool Pause { get => pause; set { pause = value; if (pause) { Time.timeScale = 0; } else { Time.timeScale = 1; } } }
    public bool Loaded { get => loaded; set { loaded = value;/*Nav.enabled=true*/  } }
    public PlayerBattleSceneMovement BattleMode { get => battleMode; set => battleMode = value; }
    public GameObject DemonSword { get => demonSword; set { demonSword = value; } }
    public GameObject HitBox { get => hitBox; set { hitBox = value; } }

    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("Skill ID", skillId); if (skillId == 0) { SkillIsActive = false; } } }
    public Rigidbody RBody { get => rBody; set => rBody = value; }

    public Animator Anim { get => anim; set => anim = value; }

    public bool PerfectGuard { get => perfectGuard; set => perfectGuard = value; }
    public GameObject ForwardHitbox { get => forwardHitbox; set => forwardHitbox = value; }
    public GameObject FireTrail { get => fireTrail; set => fireTrail = value; }
    public GameObject AoeHitbox1 { get => AoeHitbox; set => AoeHitbox = value; }
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", animations); LegControl(); } }

    public bool LockedOn { get => lockedOn; set { lockedOn = value; anim.SetBool("Lock", lockedOn); if (!LockedOn) { if (unlocked != null) unlocked(); Direction = 0; } } }

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

    public bool LightAttack { get => lightAttack; set { lightAttack = value; anim.SetBool("LightAttack", lightAttack); } }
    public bool StrongAttack { get => strongAttack; set { strongAttack = value; anim.SetBool("StrongAttack", strongAttack); } }

    public GameObject FakeAngelSword { get => fakeAngelSword; set => fakeAngelSword = value; }
    public int Style { get => style; set { style = value; anim.SetInteger("Style", style); } }

    public int SkullMask { get => skullMask; set => skullMask = value; }
    public bool Charging { get => charging; set { charging = value; anim.SetBool("Charging", charging); } }
    public int Bulbs { get => bulbs; set => bulbs = value; }
    public GameObject WoodenSword { get => woodenSword; set => woodenSword = value; }
    public Vector3 DirectionV { get => directionV; set => directionV = value; }
    public Vector2 DisplacementV { get => displacementV; set => displacementV = value; }
    public GameObject FarHitPoint { get => farHitPoint; set => farHitPoint = value; }
    public bool SkillButton { get => skillButton; set => skillButton = value; }
    public float SpeedInc { get => speedInc; set => speedInc = value; }
    public int AirCombo { get => airCombo; set { airCombo = value; anim.SetInteger("AirCombo", airCombo); } }

    public bool Blocking { get => blocking; set => blocking = value; }
    public GameObject MainCam { get => mainCam; set => mainCam = value; }
    public bool AttackState { get => attackState; set { attackState = value; anim.SetBool("Attacking",attackState); } }

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

        GameController.respawn += RestoreHealth;

        UiManager.bedTime += Sleep;

        UiManager.sealPlayerInput += StopRunning;
        GroundChecker.landed += SoundEffects;

        DoubleJump.doubleJump += SoundEffects;
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
        SceneDialogue.sealPlayerInput += StopRunning;
        FormSwitch.inviciblity += Endure;
        MovingStates.returnSpeed += MoveBro;
        KillOtherLayers.weight += LayerControl;
        BaseBehavoirs.grounded += ZeroVelocity;
        PoisonLake.poisoned += TakeDamage;

        ShadowShot.shoot += ShootShadow;
        ShootBehavior.shoot += ShootLayer;
        AttackStates.sendAttack+=RecieveAttack;
        #region Item subs
        ItemData.mask += PowerUpp;
        #endregion
        #endregion
        #region Power HookUps
        AngelicRelic.lightSpeed += Teleportto;
        DefensePowers.defense += Block;
        #endregion
        ClothesSfx = zend.GetComponent<AudioSource>();
        Anim = zend.GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();

        //anim = GetComponent<Animator>();
        battleMode = GetComponent<PlayerBattleSceneMovement>();
        headController = GetComponent<BasicHeadController>();
        effects = GetComponent<PlayerEffects>();
    }

    void Start() {
        //Stats.onStaminaChange+=StartCoroutine(StaminaRec());
        stats.Start();
        items.Start();
        Stats.onHealthChange += CheckPlayerHealth;
        guardLayer = anim.GetLayerIndex("GuardLayer");
        shootLayer = anim.GetLayerIndex("Shooting Layer");
    }
    private void OnEnable() {
        if (onPlayerEnabled != null) {
            onPlayerEnabled();
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
    }
    private void LateUpdate() {
        
    }
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
        RBody.isKinematic = false;
        Grounded = false;
    }
    private void CalculateMoveDirection() {
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
    private void CheckPlayerHealth() {
        if (stats.HealthLeft <= 0 && !dead) { Dead = true; }
    }

    #endregion

    #region new code
    private void Move() {
        DirectionV = MainCam.transform.TransformDirection(new Vector3(DisplacementV.x, 0, DisplacementV.y).normalized);
        if (DisplacementV.magnitude >= 0.1f) {
            Moving = true;
            if (!lockedOn) {
                directionV.y = 0;
                Vector3 rot = Vector3.Normalize(DirectionV);
                rot.y = 0;
                qTo = Quaternion.LookRotation(DirectionV);
                transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotationSpeed);
            }
        }
        else {
            Moving = false;
        }
    }
    private void MoveBro(float move) {
        moveSpeed = move;
        if (!lockedOn) {
            Vector3 speed;
            speed = move * DirectionV.normalized;
            speed.y = RBody.velocity.y;
            RBody.velocity = speed;
            // RBody.MovePosition(RBody.position+(speed*Time.deltaTime));
        }
        //rBody.MovePosition(rBody.position+speed);
        //charaCon.SimpleMove(speed);
    }
    private void RecieveAttack() {
        AttackState = false;
    }
    #endregion

    #region Ability Functions
    private void Teleportto() {
        if (battleMode.EnemyTarget.Grounded)
            transform.position = battleMode.EnemyTarget.gameObject.transform.position + new Vector3(0, 4, -0.5f);
        else
            transform.position = battleMode.EnemyTarget.gameObject.transform.position + new Vector3(0, 0.5f, -0.5f);
    }
    private void ShootShadow() {
        Instantiate(effects.ShadowShot, leftHand.transform.position, Quaternion.identity);
    }
    private void ShootLayer(int val) {
        anim.SetLayerWeight(shootLayer, val);
    }
    private void Block(bool val) => Guard = val;
    #endregion

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
    private void OnDeath() {
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
    }
    private IEnumerator ResetTimeStop() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        timeStopped = false;
    }
    private void AnimationLayerManagement() {
        ////anim.SetLayerWeight(2, 1);
        //if (grounded&&!lockedOn) {
        //    StartCoroutine(SetLayerWait());
        //}
        //else {
        //    anim.SetLayerWeight(3, 0);
        //}
        ////if (cmdInput > 0) {
        ////    anim.SetLayerWeight(3, 0);
        //}
    }
    private IEnumerator SetLayerWait() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        anim.SetLayerWeight(legsLayer, 1);

    }
    private void GuardBreak() {
        GuardAnimations = 3;
    }
    #region Unused Methods
    private void SetHeadWeight(float result) {
        headController.Weight = result;
    }
    #endregion




    #region Coroutines
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

    #endregion
    #region Inputs
    public void SkillSquare() {
        if (square.SkillAssigned != null && stats.MPLeft >= square.MpRequired) {
            stats.MPLeft -= square.MpRequired;
            square.UseSkill();
            skillIsActive = true;
        }
    }
    public void SkillX() {
        if (x.SkillAssigned != null && stats.MPLeft >= x.MpRequired) {
            stats.MPLeft -= x.MpRequired;
            x.UseSkill();
            skillIsActive = true;
        }
    }
    public void SkillTriangle() {
        if (triangle.SkillAssigned != null && stats.MPLeft >= triangle.MpRequired) {
            stats.MPLeft -= triangle.MpRequired;
            triangle.UseSkill();
            skillIsActive = true;
        }
    }
    public void SkillCircle() {
        if (circle.SkillAssigned != null && stats.MPLeft >= circle.MpRequired) {
            stats.MPLeft -= circle.MpRequired;
            circle.UseSkill();
            skillIsActive = true;
            //Guard = false;
        }
    }

    private void Dodge(float move) {
        RBody.velocity = transform.right * move;
    }
    private void Jump() {
        SkillId = 10;
        RBody.AddForce(new Vector3(0, 333, 0), ForceMode.Impulse);
    }
    private IEnumerator Boost() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        //RBody.useGravity = true;
        Boosting = false;
        //oveSpeed = 6;
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
    #endregion
    public void TargetingLogic(bool val) {
        if (val) {
            LockedOn = true;
            if (playerIsLockedOn != null) {
                playerIsLockedOn();
            }
            findClosestEnemy.Invoke();
            Attacking = true;
        }
        else {
            notAiming.Invoke();
            Attacking = false;
            LockedOn = false;
        }
    }
    #region Event handlers
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
        demonSwordBack.SetActive(true);
        demonScabbard.SetActive(true);
        WoodenSword.SetActive(true);
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
    private IEnumerator WaitToLand() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        InputSealed = false;
    }
    private void GroundSlamForce(float force) {
        RBody.mass = force;
    }
    private void OnGrounded(bool val) {
        Grounded = val;
        //RBody.isKinematic = val;
    }
    private void Sleep() {
        Body.SetActive(false);
        sleep = true;
        stats.HealthLeft = stats.Health;
        InputSealed = true;
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
        mask.SetActive(true);
        PoweredUp = true;
        PowerUp = true;
    }
    private void ZeroVelocity() {
        rBody.velocity = new Vector3(0, 0, 0);
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
}
