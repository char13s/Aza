using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AzaAi : MonoBehaviour
{
    private AzaAiStates state;
    public enum AzaAiStates { Idle, Attacking, FindZend, LowHealth, ReturnToSpawn, Dead, Hit };
    private NavMeshAgent navi;
    private Animator anim;
    private bool loaded;
    private int animations;
    private static AzaAi instance;

    internal Stats stats = new Stats();

    public bool Loaded { get => loaded; set { loaded = value; Navi.enabled = true; } }
    public NavMeshAgent Navi { get => navi; set => navi = value; }
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", Animations); } }

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
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private IEnumerator StateCoroutine()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(0.1f);
            StateChange();
        }
    }
    private void StateChange()
    {
        if (Vector3.Distance(Player.GetPlayer().transform.position, transform.position) > 3)
        {
            state = AzaAiStates.FindZend;
        }
        StateControl();
    }
    private void StateControl()
    {
        switch (state)
        {
            case AzaAiStates.Idle:
                Idle();
                break;
            case AzaAiStates.FindZend:
                FindZend();
                break;
        }
    }
    private void Idle()
    {
        Animations = 0;
        if (Loaded) { 
        Navi.SetDestination(transform.position);}
    }
    private void FindZend()
    {
        Animations = 1;
        transform.rotation = Quaternion.LookRotation( Player.GetPlayer().transform.position- transform.position);
        Navi.SetDestination(Player.GetPlayer().transform.position);
        if (Vector3.Distance(Player.GetPlayer().transform.position, transform.position) < 3)
        {
            state = AzaAiStates.Idle;
        }
    }
}
