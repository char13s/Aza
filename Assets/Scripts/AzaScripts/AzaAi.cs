using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AzaAi : MonoBehaviour
{
    private AzaAiStates state;
    public enum AzaAiStates { Idle, Attacking, FindZend, LowHealth, Casting, Dead, Hit };
    private NavMeshAgent navi;
    private Animator anim;
    private bool loaded;
    private int animations;
    [SerializeField] private GameObject healCast;
    [SerializeField] private GameObject azaBow;
    private static AzaAi instance;

    internal Stats stats = new Stats();

    private AxisButton R2 = new AxisButton("L2");

    public bool Loaded { get => loaded; set { loaded = value; Navi.enabled = true; } }
    public NavMeshAgent Navi { get => navi; set => navi = value; }
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", Animations); } }

    public AzaAiStates State { get => state; set => state = value; }
    public GameObject HealCast { get => healCast; set => healCast = value; }
    public GameObject AzaBow { get => azaBow; set => azaBow = value; }

    public static AzaAi GetAza() => instance.GetComponent<AzaAi>();
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
    }
    // Start is called before the first frame update
    private void Start()
    {
        stats.Start();
        Navi = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        StartCoroutine(StateCoroutine());
        StartCoroutine(StaminaRec());
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        if (R2.GetButton() && Input.GetButtonDown("Square") && stats.MPLeft >= 10)
        {
            stats.MPLeft -= 10;
            State = AzaAiStates.Casting;
            CastHeal();
        }
        if (R2.GetButton() && Input.GetButtonDown("Circle") && stats.MPLeft >= 5)
        {
            stats.MPLeft -= 5;
            State = AzaAiStates.Casting;
            FireBall();
        }
    }
    private IEnumerator StateCoroutine()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(0.1f);
            if (State != AzaAiStates.Casting)
            {
                StateChange();
            }
        }
    }
    private IEnumerator StaminaRec()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(5);
            if (stats.MPLeft < stats.MP)
            {
                stats.MPLeft += 5;
            }
        }
    }
    private void StateChange()
    {

        StateControl();
    }
    private void StateControl()
    {
        switch (State)
        {
            case AzaAiStates.Idle:
                Idle();
                break;
            case AzaAiStates.FindZend:
                FindZend();
                break;
            case AzaAiStates.Attacking:
                ShootArrows();
                break;
        }
    }
    private void ShootArrows()
    {
        
        Vector3 delta;
        Animations = 4;
        if (Player.GetPlayer().BattleMode.EnemyTarget != null)
        {
            delta = Player.GetPlayer().BattleMode.EnemyTarget.transform.position - transform.position;
        }
        else
        {
            delta = new Vector3(0, 0, 0);
        }
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        if (Player.GetPlayer().BattleMode.EnemyTarget == null) {
            State = AzaAiStates.Idle;
        }

    }
    private void FireBall()
    {
        Vector3 delta;
        Animations = 3;
        if (Player.GetPlayer().BattleMode.EnemyTarget != null)
        {

            delta = Player.GetPlayer().BattleMode.EnemyTarget.transform.position - transform.position;
        }
        else
        {
            delta = new Vector3(0, 0, 0);
        }
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        //State = AzaAiStates.Idle;
    }
    private void CastHeal()
    {
        Vector3 delta;
        Animations = 2;
        delta = Player.GetPlayer().transform.position - transform.position;
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        HealCast.SetActive(true);
        Player.GetPlayer().stats.HealthLeft += 5;
        //State = AzaAiStates.Idle;
    }
    private void Idle()
    {
        Animations = 0;
        if (Loaded)
        {
            Navi.SetDestination(transform.position);
        }
        /*if (State != AzaAiStates.Casting && Player.GetPlayer().BattleMode.EnemyTarget!=null &&Player.GetPlayer().Attacking) {
            State = AzaAiStates.Attacking;
        }*/
        if (State != AzaAiStates.Casting && Vector3.Distance(Player.GetPlayer().transform.position, transform.position) > 3)
        {
            State = AzaAiStates.FindZend;
        }
    }
    private void FindZend()
    {
        Animations = 1;
        transform.rotation = Quaternion.LookRotation(Player.GetPlayer().transform.position - transform.position);
        Navi.SetDestination(Player.GetPlayer().transform.position);
        if (Vector3.Distance(Player.GetPlayer().transform.position, transform.position) < 3)
        {
            State = AzaAiStates.Idle;
        }
    }
}
