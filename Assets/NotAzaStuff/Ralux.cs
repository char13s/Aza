using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralux : MonoBehaviour
{
    private Vector3 displacement;
    [SerializeField]private GameObject mainCam;
    [SerializeField] private float moveSpeed;
    private bool lockedOn;
    #region animation states
    private bool attack;
    private bool attackStance;
    private bool jumping;
    private bool grounded;
    #endregion
    private bool moving;
    private Rigidbody rbody;
    private Animator anim;
    private static Ralux instance;

    public static Ralux GetRal() => instance;
    public bool Moving { get => moving; set { moving = value;anim.SetBool("Moving",moving); } }

    public bool Jumping { get => jumping; set { jumping = value;anim.SetBool("Jumping",jumping); } }
    public bool AttackStance { get => attackStance; set { attackStance = value;anim.SetBool("AttackStance",attackStance); } }
    public bool Attack { get => attack; set { attack = value;anim.SetBool("Attack",attack); } }

    public bool Grounded { get => grounded; set { grounded = value;anim.SetBool("Grounded",grounded); } }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplacementControl();
        if (grounded) {
            Jump();
        }
        else {
            Jumping = false;
        }
        
        Attacking();
    }
    
    private void DisplacementControl() {
        float x = Input.GetAxisRaw("Horizontal") * 0.05f * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * 0.05f * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn) {
            displacement = mainCam.GetComponent<ThreeDCamera>().XZOrientation.TransformDirection(displacement);
        }
        MoveIt(x,y);

    }
    private void MoveIt(float x, float y) {
        if (x != 0 || y != 0) {
            Moving = true;
            Move(moveSpeed);//Move such logic to the animator
        }
        else {
            Moving = false;
        }
    }
    public void Move(float speed) {
        transform.position += displacement * speed * Time.deltaTime;
        Rotate();
    }
    private void Rotate() {
        if (Vector3.SqrMagnitude(displacement) > 0.01f) {
            transform.forward = displacement;
        }
    }
    private void Jump() {
        if (Input.GetButtonDown("X")) {
            rbody.AddForce(new Vector3(0,100,0),ForceMode.Impulse);
            Jumping = true;
            Debug.Log("Jump");
        }
        if (Input.GetButtonUp("X")) {
            Jumping = false;
        }
    }
    private void Attacking() {
        if (!AttackStance && Input.GetButtonDown("Square")) {
            AttackStance = true;
            return;
        }
        if (attackStance) {
            if (Input.GetButtonDown("Square")) {
                Attack = true;
            }
            if (Input.GetButtonUp("Square")) {
                Attack = false;
            }
        }
    }
}
