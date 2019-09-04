using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
#pragma warning disable 0649
public class Player : MonoBehaviour
{
    //private bool usingController;
    [Header("Movement")]
    private bool moving;
    //public float speed;
    [SerializeField] private float moveSpeed = 3;

    [Space]
    [Header("Attacking")]
    [SerializeField] private GameObject hitBox;
    private bool attacking;
    private bool skillButton;
    [SerializeField] private GameObject swordSpawn;
    [SerializeField] private GameObject swordDSpawn;
    private bool swordIN;
    [Space]
    [Header("rotations")]
    [SerializeField] private GameObject body;
    //public bool right;

    [Space]
    [Header("Items")]
    //public List<Items> inventory;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject pickAxe;
    [SerializeField] private GameObject guitar;

    [Space]
    [Header("Animation States")]
    private bool rockOut;
    private bool pickUp;
    private bool wall;
    private bool climbing;
    private bool chopping;
    private bool grounded;
    private bool gliding;
    private bool wallMoving;
    private bool leftDash;
    private bool rightDash;
    private bool guard;
    private bool hit;
    private bool dead;
    private int direction;
    private bool stop;
    private int skillId;
    private bool attack;
    [Space]
    [Header("OtherFunctions")]
    private Rigidbody rBody;
    private bool pause;
    private int guitarTimer = 150;
    private byte timer;
    private bool loaded;
    [SerializeField] private Material fader;
    [SerializeField] private Material normal;
    [SerializeField] private Material handle;
    [SerializeField] private Material blade;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject abilitiesUi;

    [SerializeField] private GameObject AoeHitbox;
    [SerializeField] private GameObject forwardHitbox;
    [SerializeField] private GameObject fireTrail;
    [SerializeField] private GameObject fireCaster;
    private static Player instance;


    private Coroutine guardCoroutine;
    private Coroutine hitDefuse;
    private Coroutine dodgeCoroutine;

    private int hitCounter;
    //private Vector3 delta;
    internal Inventory items = new Inventory();
    internal Stats stats = new Stats();
    private bool perfectGuard;
    private NavMeshAgent nav;
    private PlayerBattleSceneMovement BattleStuff;
    private Animator anim;

    public static event UnityAction onPlayerDeath;
    public static event UnityAction onPlayerEnabled;

    public bool RockOut { get => rockOut; set { rockOut = value; anim.SetBool("RockOut", rockOut); } }
    public bool PickUp1 { get => pickUp; set { pickUp = value; anim.SetBool("PickUp", pickUp); } }
    public bool Wall { get => wall; set => wall = value; }
    public bool Climbing1 { get => climbing; set { climbing = value; anim.SetBool("Climbing", climbing); } }
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); } }
    public bool WallMoving { get => wallMoving; set { wallMoving = value; anim.SetBool("WallMoving", wallMoving); } }
    public bool LeftDash { get => leftDash; set { leftDash = value; anim.SetBool("LeftDash", leftDash); } }
    public bool RightDash { get => rightDash; set { rightDash = value; anim.SetBool("RightDash", rightDash); } }
    public bool Guard { get => guard; set { guard = value; shield.SetActive(value); anim.SetBool("Guard", guard); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public int HitCounter { get => hitCounter; set { hitCounter = value; anim.SetInteger("counter", hitCounter); } }
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

    public PlayerBattleSceneMovement BattleStuff1 { get => BattleStuff; set => BattleStuff = value; }
    public GameObject Sword1 { get => sword; set => sword = value; }
    public GameObject HitBox { get => hitBox; set => hitBox = value; }
    public bool Attack { get => attack; set { attack = value; anim.SetBool("Attack", attack); } }
    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("Skill ID", skillId); } }
    public Rigidbody RBody { get => rBody; set => rBody = value; }
    public NavMeshAgent Nav { get => nav; set { nav = value; } }
    public Animator Anim { get => anim; set => anim = value; }
    public bool Chopping { get => chopping; set { chopping = value; anim.SetBool("Chopping", chopping); } }

    public GameObject Axe { get => axe; set => axe = value; }
    public bool PerfectGuard { get => perfectGuard; set => perfectGuard = value; }
    public GameObject ForwardHitbox { get => forwardHitbox; set => forwardHitbox = value; }
    public GameObject FireTrail { get => fireTrail; set => fireTrail = value; }
    public GameObject AoeHitbox1 { get => AoeHitbox; set => AoeHitbox = value; }

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
        Anim = GetComponent<Animator>();

    }

    void Start()
    {
        onPlayerDeath += OnDead;
        GameController.onNewGame += SetDefault;
        stats.Start();
        items.Start();
        RBody = GetComponent<Rigidbody>();
        Nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        BattleStuff1 = GetComponent<PlayerBattleSceneMovement>();
        
    }
    private void OnEnable()
    {
        if (onPlayerEnabled != null)
        {
            onPlayerEnabled();
        }
        StartCoroutine(StaminaRec());
        //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 0);
        //Delta = Cam.transform.position - body.transform.position;

    }
    // Update is called once per frame
    void Update()
    {

        if (!Attacking && !Climbing1 && Grounded && !pickUp)
        {
            GetInput();
        }

        Sword();
        Inventory();
        Guitar();
        OnPause();
        Skills();

        if (Timer > 0)
        {
            Timer--;
        }
        if (Input.GetAxis("SkillButton") > 0) {
            
            skillButton = true;
        }
        //if (Input.GetKey(KeyCode.P)) { stats.Level += 10; }
    }
    void OnDead()
    {
        //GetComponentInChildren<SkinnedMeshRenderer>().material = fader;
        //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 1);

    }
    void SetDefault()
    {
        Attacking = false;
        HitCounter = 0;
        stats.Start();
        //GetComponentInChildren<SkinnedMeshRenderer>().material = normal;
        BattleStuff1.Enemies.Clear();
        Dead = false;
    }
    void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        RotatePlayer(x, y);
        MoveIt(x * moveSpeed * Time.deltaTime, y * moveSpeed * Time.deltaTime);
    }
    private void MoveIt(float x, float y)
    {
        if (x != 0 && !Wall || y != 0 && !Wall)
        {

            Moving = true;

        }
        else
        {
            Moving = false;
        }
        if (!Wall)
        {
            transform.position += new Vector3(x, 0, y);
        }
    }

    private void RotatePlayer(float x, float y)
    {
        if (Mathf.Abs(x) < 0.01 && Mathf.Abs(y) < 0.01) return;
        Vector3 forward = new Vector3(x, 0, y);
        Quaternion nextRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = nextRotation;
    }
    private void Climbing()
    {

        float x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed / 2 * Time.deltaTime;

    }
    void Gliding()
    {


    }
    public void MoveUp() => transform.position += new Vector3(0.5f, 1.2f, 0);
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
    void Skills()
    {
        if (skillButton && stats.StaminaLeft >= 1 && Input.GetButtonDown("Triangle") && Input.GetButtonDown("Square"))
        {
            SkillId = 2;

        }
        if (skillButton && stats.StaminaLeft >= 3 && Input.GetButtonDown("Triangle"))
        {
            SkillId = 7;
            stats.StaminaLeft -= 3;
        }
        if (skillButton && stats.StaminaLeft >= 15 && Input.GetButtonDown("Square"))
        {
            SkillId = 3;
            stats.StaminaLeft -= 15;
        }
        if (skillButton&&stats.StaminaLeft>=10&&Input.GetButtonDown("Circle"))
        {
            SkillId = 1;
            stats.StaminaLeft -= 10;
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
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(1f);
            PerfectGuard = false;
            StopCoroutine(guardCoroutine);
        }
    }
    private IEnumerator HitDefuse()
    {
        yield return new WaitForSeconds(0.3f);
        Hit = false;
        StopCoroutine(hitDefuse);
    }
    private IEnumerator Dodging()
    {
        yield return null;
        LeftDash = false;
        RightDash = false;
        StopCoroutine(dodgeCoroutine);
    }
    private IEnumerator StaminaRec()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(5);

            stats.StaminaLeft += 5;
        }
    }
    private void Sword()
    {

        if (Input.GetButtonDown("R1") && !Attacking)
        {
            Attacking = true;

            return;
        }

        if (Attacking && !swordIN)
        {
            Instantiate(swordSpawn, Sword1.transform);
            Timer = 60;
            swordIN = true;
        }

        if (Attacking)
        {
            if (BattleStuff1.Enemies.Count == 0)
            {
                Attacking = false;
            }
            if (Input.GetButtonDown("Square"))
            {
                PerfectGuard = true;
                guardCoroutine = StartCoroutine(GuardCoroutine());
            }
            if (Input.GetButton("Square"))
            {
                Guard = true;
            }
            else
            {
                Guard = false;
            }

            if (skillButton && Input.GetAxis("Horizontal") < 0 && !leftDash && stats.StaminaLeft >= 5)
            {

                dodgeCoroutine = StartCoroutine(Dodging());
                LeftDash = true;
                stats.StaminaLeft -= 5;
            }

            if (skillButton && Input.GetAxis("Horizontal") > 0 && !rightDash && stats.StaminaLeft >= 5)
            {
                dodgeCoroutine = StartCoroutine(Dodging());
                RightDash = true;
                stats.StaminaLeft -= 5;
            }

            if (Input.GetButtonDown("R1"))
            {
                Attacking = false;
            }

            if (Timer == 0)
            {
                Sword1.GetComponent<MeshRenderer>().material = blade;
            }
            Sword1.SetActive(true);
            trail.SetActive(true);

            if (Input.GetButtonDown("X") && stats.StaminaLeft > 0)
            {
                Attack = true;
            }
        }
        else
        {
            Sword1.GetComponent<MeshRenderer>().material = handle;
            trail.SetActive(false);
            HitBox.SetActive(false);
            skillId = 0;
        }
        if (Input.GetButtonUp("R1"))
        {
            byte stimer = 60;
            Instantiate(swordDSpawn, Sword1.transform);
            swordIN = false;
            if (stimer > 0)
            {
                stimer--;
            }
            if (stimer <= 0)
            {
                Sword1.SetActive(false);
            }
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
        items.AddItem(other.gameObject.GetComponent<Items>().data);
        other.gameObject.SetActive(false);
        Destroy(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            items.AddItem(other.gameObject.GetComponent<Items>().data);
            other.gameObject.SetActive(false);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Log"))
        {
            if (Input.GetButtonDown("X"))
            {
                Debug.Log("ok and?");

                Chopping = true;

            }
        }
    }
}
