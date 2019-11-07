using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Animations;
#pragma warning disable 0649
public class Player : MonoBehaviour
{
    //private bool usingController;
    [Header("Movement")]
    private bool moving;
    //public float speed;
    [SerializeField] private float moveSpeed;
    private int money;
    #region Attacking
    [Space]
    [Header("Attacking")]
    [SerializeField] private GameObject hitBox;
    private bool attacking;
    private bool skillButton;
    private bool lockedOn;
    [SerializeField] private GameObject swordSpawn;
    [SerializeField] private GameObject swordDSpawn;

    private bool skillIsActive;
    [SerializeField] GameObject aimmingPoint;
    #endregion
    [Space]
    [Header("rotations")]
    [SerializeField] private GameObject body;
    //public bool right;
    #region Items
    [Space]
    [Header("Items")]
    //public List<Items> inventory;
    [SerializeField] private GameObject demonSword;
    [SerializeField] private GameObject demonSwordBack;
    [SerializeField] private GameObject guitar;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject attackBow;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject axe;
    #endregion
    #region Animation States
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

    private int cmdInput;
    private int animations;
    private bool bowUp;
    private bool poweredUp;
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
    [SerializeField] private GameObject aza;
    [SerializeField] private GameObject zend;
    [SerializeField] private GameObject zendHair;
    [SerializeField] private GameObject zendHead;

    [SerializeField] private RuntimeAnimatorController azaAnimatorController;

    [SerializeField] private Material fader;
    [SerializeField] private Material normal;
    [SerializeField] private Material handle;
    [SerializeField] private Material blade;




    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject abilitiesUi;

    [SerializeField] private GameObject AoeHitbox;
    [SerializeField] private GameObject forwardHitbox;
    [SerializeField] private GameObject fireTrail;
    [SerializeField] private GameObject fireCaster;
    #endregion
    [Space]
    [Header("Buttons")]
    [SerializeField] private SkillButton triangle;
    [SerializeField] private SkillButton circle;
    [SerializeField] private SkillButton square;
    [SerializeField] private SkillButton x;
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
    internal Stats stats = new Stats();
    private AxisButton dPadUp = new AxisButton("DPad Up");
    private AxisButton R2 = new AxisButton("R2");
    private AxisButton L2 = new AxisButton("L2");
    internal StatusEffects status = new StatusEffects();
    #endregion
    #region Events
    public static event UnityAction aiming;
    public static event UnityAction onPlayerDeath;
    public static event UnityAction onPlayerEnabled;
    public static event UnityAction playerIsLockedOn;
    public static event UnityAction onCharacterSwitch;
    public static event UnityAction notAiming;
    #endregion
    //Optimize these to use only one Animation parameter in 9/14
    #region Getters and Setters
    public bool RockOut { get => rockOut; set { rockOut = value; anim.SetBool("RockOut", rockOut); } }
    public bool PickUp1 { get => pickUp; set { pickUp = value; anim.SetBool("PickUp", pickUp); } }
    public bool Wall { get => wall; set => wall = value; }
    public bool Climbing1 { get => climbing; set { climbing = value; anim.SetBool("Climbing", climbing); } }
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); } }

    public bool WallMoving { get => wallMoving; set { wallMoving = value; anim.SetBool("WallMoving", wallMoving); } }
    public bool LeftDash { get => leftDash; set { leftDash = value; anim.SetBool("LeftDash", leftDash); } }
    public bool RightDash { get => rightDash; set { rightDash = value; anim.SetBool("RightDash", rightDash); } }
    public bool Guard { get => guard; set { guard = value; if (value) Moving = false; shield.SetActive(value); anim.SetBool("Guard", guard); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }

    public byte Timer { get => timer; set => timer = value; }
    public GameObject Body { get => body; set => body = value; }
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); if (hit) { hitDefuse = StartCoroutine(HitDefuse()); } } }
    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); if (dead && onPlayerDeath != null) { onPlayerDeath(); } } }
    public int Direction { get => direction; set { direction = value; anim.SetInteger("Direction", direction); } }
    //public Vector3 Delta { get => delta; set => delta = value; }
    public GameObject AbilitiesUi { get => abilitiesUi; set => abilitiesUi = value; }
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
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", animations); if (animations == 1) { Debug.Log(clothesSfx); clothesSfx.volume = 0.5f; } else { clothesSfx.time = 0; clothesSfx.volume = 0; } } }

    public bool LockedOn { get => lockedOn; set { lockedOn = value; if (LockedOn) { if (playerIsLockedOn != null) playerIsLockedOn(); } if (!LockedOn) Direction = 0; } }

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
    #endregion
    public static Player GetPlayer() => instance.GetComponent<Player>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        sfx = GetComponent<AudioSource>();
        clothesSfx = zend.GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        battleMode = GetComponent<PlayerBattleSceneMovement>();
        headController = GetComponent<BasicHeadController>();
        GameController.onNewGame += SetDefault;
        onPlayerDeath += OnDead;
        Dialogue.dialogueUp += DialogueUp;
    }

    void Start()
    {
        //Stats.onStaminaChange+=StartCoroutine(StaminaRec());
        stats.Start();
        items.Start();
        Stats.onHealthChange += CheckPlayerHealth;
        
        grounded = anim.GetBool("Grounded");
    }
    private void OnEnable()
    {
        if (onPlayerEnabled != null)
        {
            onPlayerEnabled();
        }
        staminaRec=StartCoroutine(StaminaRec());
    }
    // Update is called once per frame
    void Update()
    {
        Archery();

        if (grounded && !guard && !lockedOn && moveSpeed > 0)
        {
            GetInput();
        }
        else
        {
            displacement = Vector3.zero;
        }
        CalculateMoveDirection();


        
            Sword();
        

        //Inventory();
        //Guitar();
        OnPause();
        Skills();

        if (attacking && R2.GetButton())
        {
            skillButton = true;
        }
        else
            skillButton = false;
        //if (Input.GetKey(KeyCode.P)) { stats.Level += 10; }
    }
    private void CalculateMoveDirection()
    {
        Vector3 right;
        if (movementBone != null)
        {

            right = movementBone.TransformDirection(moveBoneRight);
        }
        else
        {
            right = transform.forward;
        }
        float dot = Vector3.Dot(right, displacement);
        anim.SetFloat("MoveDirectionX", dot);
    }
    private void OnDead()
    {
        //GetComponentInChildren<SkinnedMeshRenderer>().material = fader;
        //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 1);

    }
    private void SetDefault()
    {
        PostProcessorManager.GetProcessorManager().Default();
        Attacking = false;
        CmdInput = 0;
        MoveSpeed = 5;
        LockedOn = false;
        stats.Start();
        FireTrail.SetActive(false);
        mask.SetActive(false);
        GameObject aura = transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(aura);
        //GetComponentInChildren<SkinnedMeshRenderer>().material = normal;
        battleMode.Enemies.Clear();
        Dead = false;
        Money = 1000;
    }
    private void DialogueUp() {

        MoveSpeed = 0;


    }
    void SwitchCharacter()
    {
        if (zend.activeSelf)
        {
            zend.SetActive(false);
            aza.SetActive(true);
            transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        else
        {
            zend.SetActive(true);
            aza.SetActive(false);
            transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            return;
        }
    }
    private void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn)
        {
            displacement = ThreeDCamera.XZOrientation.TransformDirection(displacement);
        }
        /*if (Input.GetButtonDown("R3"))
        {
            //Grounded = true;
            if (onCharacterSwitch != null)
                onCharacterSwitch();
            SwitchCharacter();
        }*/
        if (Input.GetButtonDown("R3") && !transforming&&stats.MPLeft>=1)
        {
            if (poweredUp)
            {
                Deform();
                //stats.MPLeft--;
            }
            else
            {
                PowerDown();
                
            }
            


        }
        if (poweredUp&&stats.MPLeft==0) {
                Deform();
            }
        WeaponSwitch();

        MoveIt(x, y);


    }
    private void Deform() {
        PostProcessorManager.GetProcessorManager().Default();
                PoweredUp = false;
                stats.Attack /= 2;
                stats.Defense /= 2;
                MoveSpeed /= 2;
                GameObject aura = transform.GetChild(transform.childCount - 1).gameObject;
                Instantiate(swordDSpawn, transform);
                FireTrail.SetActive(false);
                mask.SetActive(false);
                Destroy(aura);
                StopCoroutine(mpDrain);
                staminaRec =StartCoroutine(StaminaRec());
                Debug.Log("Powered");


    }
    private void PowerDown()
    {mpDrain=StartCoroutine(MpDrain());
        StopCoroutine(staminaRec);
        FireTrail.SetActive(true);
            mask.SetActive(true);
            PoweredUp = true;
            PowerUp = true;

    }
    private void WeaponSwitch()
    {
        if (L2.GetButtonDown() || Input.GetKeyDown(KeyCode.F))
        {
            if (!bowUp && !attacking)
            {

                Debug.Log("fuck");
                //demonSwordBack.GetComponent<MeshFilter>().mesh = bow.GetComponentInChildren<MeshFilter>().sharedMesh;

            }
            else
            {

                //bow.GetComponentInChildren<MeshFilter>().mesh =demonSwordBack.GetComponent<MeshFilter>().sharedMesh;

            }
        }
    }
    private void Archery()
    {


        if (Input.GetButton("Square"))
        {

        }
        if (Input.GetButtonUp("Square"))
        {

        }
        if (L2.GetButtonDown())
        {
			Attacking = false;
            //targeting = true;
            BowUp = true;
            AttackBow.SetActive(true);
            MoveSpeed = 2;
            CmdInput = 5;
            if (aiming != null)
            {
                aiming();
            }

            StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 1, 0.2f, SetHeadWeight));
        }
        if (L2.GetButton())
        {
            targeting = true;



        }

        if (L2.GetButtonUp())
        {

            CmdInput = 6;
            MoveSpeed = 5;
            targeting = false;

        }
        if (R2.GetButtonDown()) {

        }
        if (!bowUp)
        {
            StartCoroutine(SetLayerWeightCoroutine(archeryLayerIndex, 0, 0.2f, SetHeadWeight));///GOOD CODE!!!!!

        }        //anim.


        //anim.SetLookAtPosition();
        //anim.SetLookAtWeight(testWeight, testBodyWeight, testHeadWeight, testEyesWeight, testClampWeight);
        headBone.transform.LookAt(ThreeDCamera.Retical.transform.position);

        if (targeting)
        {
            //anim.GetBoneTransform(HumanBodyBones.Spine).transform.LookAt(ThreeDCamera.Retical.position);



        }



    }

    private void SetHeadWeight(float result)
    {
        headController.Weight = result;
    }
    private IEnumerator SetLayerWeightCoroutine(int layerIndex, float weight, float duration, UnityAction<float> onFade)
    {
        float localTime = 0;
        float start = anim.GetLayerWeight(layerIndex);
        float deltaWeight = weight - start;
        float result = start;
        while (localTime < duration)
        {
            result = start + (localTime / duration) * (deltaWeight);
            anim.SetLayerWeight(layerIndex, result);
            if (onFade != null)
            {
                onFade(result);
            }
            yield return null;
            localTime += Time.deltaTime;

        }
        anim.SetLayerWeight(layerIndex, weight);
        if (onFade != null)
        {
            onFade(weight);
        }
    }
    private void MoveIt(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            //Moving = true;
            Animations = 1;

            Move(MoveSpeed);
            if (bowUp && !targeting)
            {

                StartCoroutine(StopTargeting());

            }

            if (attacking && Input.GetButtonDown("Square"))
            {

            }
        }
        else
        {

            Animations = 0;
            //Moving = false;
        }
    }
	private void BowDown() {
		if (notAiming != null) {
			notAiming();

		}

		AttackBow.SetActive(false);
		BowUp = false;

	}
    private IEnumerator StopTargeting()
    {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
		BowDown();


    }
    public void Move(float speed)
    {
        transform.position += displacement * speed * Time.deltaTime;
        if (Vector3.SqrMagnitude(displacement) > 0.01f)
        {
            transform.forward = displacement;
        }
    }

    private void Inventory()
    {
        if (items.PocketActive)
        {
            if (Input.GetKeyDown(KeyCode.E) && items.Page < 3)
            {

                items.Page++; Debug.Log(items.Page);
                items.DisplayInventory();
            }
            if (Input.GetButtonDown("L1") && items.Page > 0)
            {

                items.Page--; Debug.Log(items.Page);
                items.DisplayInventory();
            }
        }
    }
    private void CheckPlayerHealth()
    {

        if (stats.HealthLeft <= 0) { Dead = true; }
    }
    private void Skills()
    {
        if (skillButton && Input.GetButtonDown("Triangle") && !skillIsActive)
        {
            

            if (triangle.SkillAssigned != null && stats.MPLeft >= triangle.MpRequired)
            {
                stats.MPLeft -= triangle.MpRequired;
                triangle.UseSkill();
                skillIsActive = true;


            }

        }

        if (skillButton && Input.GetButtonDown("Square") && !skillIsActive)
        {
            if (square.SkillAssigned != null && stats.MPLeft >= square.MpRequired)
            {
                stats.MPLeft -= square.MpRequired;
                square.UseSkill();
                skillIsActive = true;


            }

        }
        if (skillButton && Input.GetButtonDown("Circle") && !skillIsActive)
        {

            if (circle.SkillAssigned != null && stats.MPLeft >= circle.MpRequired)
            {
                stats.MPLeft -= circle.MpRequired;
                circle.UseSkill();
                skillIsActive = true;
                Guard = false;


            }

        }
        if (skillButton && Input.GetButtonDown("X") && !skillIsActive)
        {
            if (x.SkillAssigned != null && stats.MPLeft >= x.MpRequired)
            {
                stats.MPLeft -= x.MpRequired;
                x.UseSkill();
                skillIsActive = true;


            }

        }
        if (stats.MPLeft >= 2 && !bowUp&&!skillIsActive && Input.GetButtonDown("X") && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            SkillId = 10;
            stats.MPLeft -= 2;
            skillIsActive = true;

        }
    }
    void Guitar()
    {
        if (Input.GetKey(KeyCode.R) && items.HasItem(2))
        {

            RockOut = true;
        }
    }
    private IEnumerator GuardCoroutine()
    {

        YieldInstruction wait = new WaitForSeconds(1f);
        yield return
        PerfectGuard = false;
        StopCoroutine(guardCoroutine);

    }
    private IEnumerator HitDefuse()
    {
        YieldInstruction wait = new WaitForSeconds(0.3f);
        yield return
        Hit = false;
        StopCoroutine(hitDefuse);
    }
    private IEnumerator StaminaRec()
    {

        while (isActiveAndEnabled)
        {
            YieldInstruction wait = new WaitForSeconds(3);
            yield return wait;
            Debug.Log("Mp up");
            if (stats.MPLeft < stats.MP)
            {
                stats.MPLeft += 5;
            }
        }

    }
    private IEnumerator MpDrain() {
        while (isActiveAndEnabled) { 
        YieldInstruction wait = new WaitForSeconds(1.5f);
        yield return wait;
        stats.MPLeft--;}
        
    }
    private void Sword()
    {

        if (Input.GetButtonDown("Square") && !Attacking)
        {
            Attacking = true;
            demonSwordBack.SetActive(false);
			CmdInput = 0;
			MoveSpeed = 5;
			targeting = false;
			BowDown();
			return;
        }
        if (Attacking)
        {
            if (Input.GetButtonDown("R1") && BattleMode.Enemies.Count > 0)
            {
                Animations = 0;
            }
            if (Input.GetButton("R1") && BattleMode.Enemies.Count > 0)
            {
                LockedOn = true;
            }
            else
            {
                LockedOn = false;
            }

            if (Input.GetButtonDown("Circle") && !skillIsActive)
            {
                PerfectGuard = true;
                guardCoroutine = StartCoroutine(GuardCoroutine());
            }
            if (Input.GetButton("Circle") && !skillIsActive)
            {
                Guard = true;
                Animations = 0;
            }
            else
            {
                Guard = false;
            }

            if (Input.GetButtonDown("L1"))
            {
                Debug.Log("attacking is false");
                Attacking = false;
                LockedOn = false;
                return;
            }

            DemonSword.SetActive(true);
            trail.SetActive(true);

            if (Input.GetButtonDown("Square") && !skillIsActive)
            {
                CmdInput = 1;
            }
            if (Input.GetButtonDown("Triangle") && !skillIsActive)
            {
                CmdInput = 2;
            }
        }
        else
        {

            trail.SetActive(false);
            HitBox.SetActive(false);
            SkillId = 0;
            DemonSword.SetActive(false);
            demonSwordBack.SetActive(true);

        }
    }
    private void OnPause()
    {
        if (Input.GetButtonDown("Pause") && !Pause)
        {
            pauseMenu.SetActive(true);
            Pause = true;
            return;
        }
        if (Input.GetButtonDown("Pause") && Pause)
        {
            pauseMenu.SetActive(false);
            Pause = false;
            return;
        }
    }
    public void PickUp(Items other)
    {
        PickUp1 = true;
        Wall = false;
        Timer = 5;
        items.AddItem(other.GetComponent<Items>().data);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            items.AddItem(other.GetComponent<Items>().data);
            other.gameObject.SetActive(false);
            Destroy(other);
        }
    }
}
