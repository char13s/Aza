using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class WeakZend : MonoBehaviour {
    [SerializeField] private GameObject mainCam;
    private Animator anim;
    private NavMeshAgent nav;
    private Rigidbody rbody;
    private static WeakZend instance;
    private Vector3 displacement;
    
    [SerializeField]private float moveSpeed;
    [SerializeField] private GameObject woodSword;
    [SerializeField] private GameObject hitbox;
    #region Animation shit
    private bool moving;
    private int cmdInput;

    private bool attacking;

    #endregion
    public bool Moving { get => moving; set { moving = value;anim.SetBool("Moving",moving); } }

    public int CmdInput { get => cmdInput; set { cmdInput = value;anim.SetInteger("CmdInput",cmdInput); } }

    public bool Attacking { get => attacking; set { attacking = value;anim.SetBool("Attacking",attacking); } }

    public Rigidbody Rbody { get => rbody; set => rbody = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public NavMeshAgent Nav { get => nav; set => nav = value; }
    public GameObject Hitbox { get => hitbox; set => hitbox = value; }

    public static WeakZend GetWeakZend() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void Start() {

    }

    // Update is called once per frame
    private void Update() {
        GetInput();
    }
    private void GetInput() {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));

        if (x != 0 || y != 0) {
            Moving = true;
        }
        else {
            Moving = false;
        }
        Move();
        Rotate();
        SwordControls();
    }
    private void Move() {
        transform.position += displacement * MoveSpeed * Time.deltaTime;
        Rotate();
    }
    private void Rotate() {
        if (Vector3.SqrMagnitude(displacement) > 0.01f) {
            transform.forward = displacement;
        }
    }
    private void ConnectNavMesh() {
        Nav.enabled = true;
    }
    private void SwordControls() {
        if (Input.GetButtonDown("Square")&&!Attacking) {
            Attacking = true;

        }
        if (attacking) {
            if (Input.GetButtonDown("Square")) {
                CmdInput = 1;
            }
            if (Input.GetButtonDown("Triangle")) {
                CmdInput = 2;
            }

        }
    }
}
