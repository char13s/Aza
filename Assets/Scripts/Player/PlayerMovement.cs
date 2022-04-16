using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected GameObject mainCam;
    private float moveSpeed;
    protected Vector3 direction;
    private Vector2 displacement;
    protected Quaternion qTo;
    private Vector3 speed;

    // gravity variables
    float gravity = -9.8f;
    float groundedGravity = -0.5f;
    // jump pamrs
    #region Jump parms
    bool isJumpPressed;
    float intialJumpVelocity;
    [SerializeField] float maxJumpHeight = 8;
    [SerializeField] float maxJumpTime = .75f;
    [SerializeField] float fallMultipler;
    bool isJumping = false;
    #endregion
    #region anim paramters
    private bool moving;
    bool isFalling;
    protected bool grounded;
    //protected bool lockedOn;
    private bool skillButton;
    private int rolls;
    private bool doubleJump;
    //public bool Grounded { get => grounded; set { grounded = value; Anim.SetBool("Grounded", grounded); } }
    public bool Moving { get => moving; set { moving = value; Anim.SetBool("Moving", moving); } }//  } }
    //public bool LockedOn { get => lockedOn; set { lockedOn = value; Anim.SetBool("LockedOn", lockedOn); } }
    public bool SkillButton { get => skillButton; set => skillButton = value; }
    #endregion
    #region Outside Scripts
    //private DefaultInputs inputs;
    private Animator anim;
    private Player player;
    private CharacterController charCon;
    //private PlayerEffects effects;
    //private PlayerInput map;
    //internal PlayerStats stats = new PlayerStats();
    // private PlayerLockOn lockon;
    #endregion
    #region Getters and Setters
    public Animator Anim { get => anim; set => anim = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Vector2 Displacement { get => displacement; set => displacement = value; }

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public CharacterController CharCon { get => charCon; set => charCon = value; }
    public bool IsJumpPressed { get => isJumpPressed; set => isJumpPressed = value; }
    public bool IsFalling { get => isFalling; set { isFalling = value; anim.SetBool("isFalling", isFalling); } }
    #endregion
    private void Start() {
        player = GetComponent<Player>();
        Anim = player.Anim;
        CharCon = GetComponent<CharacterController>();
        if (anim != null) {
            print("Anim aquired");
        }
        else {
            print("fuck that anim");
        }
        SetUpJump();
        MovingStates.returnSpeed += MoveBro;
        //PlayerAnimationEvents.setjump += Jumping;
        //DashBehavior.dash += Dash;
    }
    private void Update() {
        Rotate();
        //print(speed);
        //print(charCon.isGrounded);
        charCon.Move(speed * Time.deltaTime);
        Anim.SetBool("Grounded", charCon.isGrounded);
        Gravity();
        HandleJump();
    }
    private void Gravity() {
        IsFalling = speed.y <= 0.0f;
        //float fallMultipler =0.5f;

        if (charCon.isGrounded) {
            speed.y = groundedGravity;
        }
        else if (IsFalling) {
            float prevVelocity = speed.y;
            float newVelocity = speed.y + (gravity * fallMultipler * Time.deltaTime);
            float nextVelocity = (prevVelocity + newVelocity) * .5f;
            speed.y = nextVelocity;
        }
        else {
            float prevVelocity = speed.y;
            float newVelocity = speed.y + (gravity * Time.deltaTime);
            float nextVelocity = (prevVelocity + newVelocity) * .5f;
            speed.y = nextVelocity;
        }
    }
    private void Rotate() {
        Direction = mainCam.transform.TransformDirection(new Vector3(Displacement.x, 0, Displacement.y).normalized);
        if (Displacement.magnitude >= 0.1f) {
            Moving = true;
            direction.y = 0;
            Vector3 rot = Vector3.Normalize(Direction);
            rot.y = 0;
            qTo = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotationSpeed);
        }
        else {
            Moving = false;
        }

    }
    void HandleJump() {
        if (!isJumping && charCon.isGrounded && isJumpPressed) {
            isJumping = true;
            speed.y = intialJumpVelocity * .5f;
            anim.SetTrigger("Jump");
            //Debug.Log("ran Jump"+intialJumpVelocity);
        }
        else if (isJumping && charCon.isGrounded && !isJumpPressed) {
            isJumping = false;
        }
    }
    private void MoveBro(float move) {

        Vector3 vector = Direction.normalized;
        speed.x = move * vector.x;
        speed.z = move * vector.z;
        print("Moving bro");
        //speed = move * Direction.normalized;
        //speed.y = RBody.velocity.y;
        //RBody.velocity = speed;
        // RBody.MovePosition(RBody.position+(speed*Time.deltaTime));

        //rBody.MovePosition(rBody.position+speed);

    }
    void SetUpJump() {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        intialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

    }
    public void Jump() {
        //IsJumpPressed = true;
        print("Jump pressed");
        anim.SetTrigger("Jump");
    }
    private void Jumping(bool val) {
        IsJumpPressed = val;
    }
    private void Dash() {
        charCon.Move(transform.forward * -35 * Time.deltaTime);
    }
}