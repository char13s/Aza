using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class AIKryll : MonoBehaviour
{
    [SerializeField] private GameObject kryllCam;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject restSpot;

    public enum AIState {Idle, Follow, Attacking, PowerUp, StatusInduce, Help  }
    private AIState state;

    private bool kryll;
    private Vector3 displacement;
    private int animStates;

    private Player player;
    private Animator anim;

    public static event UnityAction zend;
    public static event UnityAction<string> sendDist;
    public static UnityAction disableCollider;
    public static event UnityAction<Vector3,int> teleport;

    private AxisButton L3 = new AxisButton("L3");
    private AxisButton DpadUp = new AxisButton("DPad Up");
    private float maxTeleportLength=5;
    private float distFromZend;
    public bool Kryll
    {
        get => kryll; set
        {
            kryll = value;
            if (!kryll)
            {
                if (zend != null)
                {
                    zend();
                }
            }
        }
    }

    public AIState State { get => state; set { state = value; } }

    public int AnimStates { get => animStates; set { animStates = value;anim.SetInteger("Animation",animStates); } }

    private void Awake()
    {
        anim=GetComponent<Animator>();
        Player.kryll += SwitchStyle;
        Player.onPlayerEnabled += Spawn;
        Player.battleOn += BattleOn;
        AreaTransition.transition += Spawn;
        Bed.bed += Sleep;
        PortalManager.backToBase += TeleportWithZend;
        disableCollider += DisableCollider;
        
    }

    private void SwitchStyle()
    {
        kryllCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        Kryll = true;
    }
    private void SwitchBack()
    {
        kryllCam.GetComponent<CinemachineVirtualCamera>().Priority = 9;
        Debug.Log("MUDA");
        Kryll = false;
        Debug.Log(kryll);
    }
    private void Sleep(GameObject location,GameObject meh) {
        //transform.position
        AnimStates = 4;
    }
    private void BattleOn()
    {
        State = AIState.Attacking;
    }
    private void TeleportWithZend(Vector3 k,bool c) {
        Debug.Log("awww");
        transform.position = spawn.transform.position;
    }
    private void Spawn()
    {
        player = Player.GetPlayer();
        transform.position = spawn.transform.position; 
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!kryll)
        {
            AIControls();
        }
        else
        {
            PlayerControls();
        }
    }
    private void AIControls()
    {
        SwitchStates();

    }
    private void PlayerControls()
    {
        Debug.Log("mudamudamuda");
        Movement();
        GetInput();
        DistanceTracker();
    }
    #region Helper Methods
    private float Distance() {
        if (player != null)
        {
            return Vector3.Distance(Player.GetPlayer().transform.position, transform.position);

        }
        else
            return 5;

    }
    private void DisableCollider() {

        GetComponent<SphereCollider>().enabled = false;
    }
    private void DistanceTracker() {

        
        if (sendDist != null) {
            sendDist(Distance().ToString());
        }
    }
    #endregion
    
    #region Player Controls
private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));

        displacement = kryllCam.GetComponent<ThreeDCamera>().XZOrientation.TransformDirection(displacement);
        if (Input.GetButtonDown("L3"))
        {
            SwitchBack();

        }
        Move(x, y);
    }
    private void Move(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            AnimStates = 1;
            transform.position += displacement * 7 * Time.deltaTime;
            if (Vector3.SqrMagnitude(displacement) > 0.01f)
            {
                transform.forward = displacement;
            }
        }

    }
    private void GetInput()
    {
        if (Input.GetButtonDown("Triangle")&&Distance()<maxTeleportLength) {
            Player.GetPlayer().transform.position = transform.position;
            
        }

    }
    #endregion
    #region AI States
    private void SwitchStates() {
        switch (state)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Follow:
                Follow();
                break;
            case AIState.Attacking:
                Attacking();
                break;
            case AIState.StatusInduce:
                InduceStatus();
                break;
            case AIState.Help:
                Help();
                break;
        }
    }
    private void Idle() {
        if (Distance() <= 1)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, restSpot.transform.position, 2 * Time.deltaTime);
            
            
            if (transform.position==restSpot.transform.position)
            {
                transform.rotation=spawn.transform.rotation;
                AnimStates = 2;
            }
            else
            {
                AnimStates = 0;
                transform.LookAt(restSpot.transform.position);

            }
        }
        else {
            State = AIState.Follow;
        }
    }
    private void Follow() {
        if (Distance() >= 1)
        {
            transform.rotation = spawn.transform.rotation;
            transform.position=Vector3.MoveTowards(transform.position, spawn.transform.position, 5.9f*Time.deltaTime);
            AnimStates = 1;
        }
        else {
            
            StartCoroutine(WaitForAwakes());
        }
    }
    private void Attacking() {
        AnimStates = 0;
        int action = Random.Range(1,60);
        switch (action) {
            
            case 3:
                State = AIState.Help;
                break;
           
            case 9:
                State = AIState.Help;
                break;
           
            default:break;
        }
    }
    private void Help() {
        
        int rand = Random.Range(0,player.GetComponent<PlayerBattleSceneMovement>().Enemies.Count);
        Enemy target= player.GetComponent<PlayerBattleSceneMovement>().Enemies[rand];
        if (!target.Dead)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 1, 0), 5.9f * Time.deltaTime);
        }
        else {
            State = AIState.Attacking;
        }
        if (DpadUp.GetButtonDown()) {

            if (teleport != null) {
                teleport(transform.position,rand);
            }
            State = AIState.Attacking;
        }
        StartCoroutine(AttackingCoroutine());
    }
    private void InduceStatus() {

    }
    #endregion
    #region Coroutines
    private IEnumerator WaitForAwakes() {
        YieldInstruction wait = new WaitForSeconds(5f);
        yield return wait;
        State = AIState.Idle;
    }
    private IEnumerator AttackingCoroutine() {
        YieldInstruction wait = new WaitForSeconds(10f);
        yield return wait;
        State = AIState.Attacking;

    }
    #endregion
}
