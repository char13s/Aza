using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerCommands : MonoBehaviour
{
    private enum Inputs { Null, X, Square, Triangle, Circle, Up, Down, Right, Left, Direction }
    #region Events
    public static event UnityAction<string> sendInput;
    public static event UnityAction circle;
    public static event UnityAction upCircle;
    public static event UnityAction downCircle;
    public static event UnityAction holdCircle;
    public static event UnityAction triangle;
    public static event UnityAction upTriangle;
    public static event UnityAction downTriangle;
    public static event UnityAction holdTriangle;
    //public static event UnityAction<int> sendChain;
    #endregion

    private Coroutine fakeUpdate;
    private bool lockon;
    #region Outside Scripts
    private Animator anim;
    private Animator animObject;
    private Player player;
    private PlayerTimelineControl timelines;
    private PlayerInputs playerInputs;
    private PlayerMovement playerMove;
    #endregion
    #region Anim parameters
    private int chain;
    private int movementChain;
    private Vector2 stick;
    #endregion
    [SerializeField] private Inputs inputs;
    [SerializeField] private Inputs direction;
    public int Chain { get => chain; set { chain = value; anim.SetInteger("ChainInput", chain); } }

    public int MovementChain { get => movementChain; set { movementChain = value; /*anim.SetInteger("MoveInput", movementChain); */} }

    private void Awake() {
        //inputs = new List<Inputs>(52);
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMovement>();
        animObject = GetComponent<Animator>();
        timelines = GetComponent<PlayerTimelineControl>();
        playerInputs = GetComponent<PlayerInputs>();
        BaseBehavoirs.baseB += ResetMoveChain;
        ChainInput.resetChain += emptyChain;

    }
    private void OnEnable() {
        //fakeUpdate=StartCoroutine(SlowUpdate());
    }
    private void OnDisable() {
        //StopCoroutine(SlowUpdate());
    }
    void Start() {
        anim = player.Anim;
    }
    private void Update() {
        //if (!player.SkillButton) {
        GetInputs();
        if (stick.sqrMagnitude == 0) {
            ResetDirection();
        }
    }
    private void GetInputs() {
        InputChains();
        if (player.LockedOn) {
            if (player.CharCon.isGrounded) {
                InputCombinations();
            }
            else {
                AirCombinations();
            }

            AdvancedMovement();
            if (player.CombatAnimations == 0) {
                MovementInputs();
            }
        }
    }
    private IEnumerator EmptyChain() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        ResetChain();
    }
    private IEnumerator SlowUpdate() {

        YieldInstruction wait = new WaitForSeconds(0.5f);
        while (isActiveAndEnabled) {
            yield return wait;
            ResetChain();
        }
    }

    private void OnMovement(InputValue value) {

        stick = value.Get<Vector2>();
        Vector2 min = new Vector2(0.01f, 0.01f);
    }
    private void OnDash(InputValue value) {
        //Chain = 7;
    }
    private void OnUp(InputValue value) {
        direction = Inputs.Up;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnDown(InputValue value) {
        direction = Inputs.Down;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnRight(InputValue value) {
        direction = Inputs.Right;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnLeft(InputValue value) {
        direction = Inputs.Left;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnJump(InputValue value) {
        if (value.isPressed) {
            if (sendInput != null) {
                sendInput("X");
            }
            if (player.CombatAnimations == 0 && player.Grounded) {
                anim.SetTrigger("Jump");
            }
            StartCoroutine(EmptyChain());
            AddInput(Inputs.X);
        }
    }
    private void OnEnergy() {
        if (sendInput != null) {
            sendInput("Triangle");
        }
        AddInput(Inputs.Triangle);
        if (triangle != null) {
            triangle();
        }
        StartCoroutine(EmptyChain());
    }
    private void OnAttack(InputValue value) {
        player.AttackState = true;
        if (sendInput != null) {
            sendInput("Square");
        }
        if (value.isPressed) {
            AddInput(Inputs.Square);
        }
        StartCoroutine(EmptyChain());
    }
    private void OnAbility() {

        if (sendInput != null) {
            sendInput("Circle");
        }
        AddInput(Inputs.Circle);
        if (circle != null) {
            circle();
        }
        StartCoroutine(EmptyChain());
    }
    private void OnHoldCircle() {
        if (holdCircle != null) {
            holdCircle();
        }
    }
    private void On() {
        Chain = 16;
    }
    private void OnHoldAttack() {
        //timelines.PlayHoldAttack();
    }
    private void OnHoldEnergy() {
        Chain = 17;
    }
    private void OnHoldTriangle(InputValue value) {
        Chain = 5;
        if (holdTriangle != null) {
            holdTriangle();
        }
        timelines.PlayHoldEnergy();
    }
    private void AddInput(Inputs button) {
        //if (!player.SkillButton) {
        inputs = button;
        //}
    }
    private void InputChains() {

        if (inputs == Inputs.Triangle && inputs == Inputs.Circle) {
            Debug.Log("Fire!BIcth");
            Chain = 9;
            ResetChain();
        }
        if (inputs == Inputs.X && direction != Inputs.Right && direction != Inputs.Left) {
            Chain = 1;
            //anim.SetTrigger("Jump");
            //ResetChain();
        }
    }
    private void AirCombinations() {
        if (inputs == Inputs.Square && direction == Inputs.Up) {
            Debug.Log("Up Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            anim.ResetTrigger("Attack");
            anim.Play("AirDive");

        }
        if (inputs == Inputs.Square && direction == Inputs.Down) {
            Debug.Log("Down Attack!");
            if (sendInput != null) {
                sendInput("Down + Square");
            }
            ResetChain();
            anim.ResetTrigger("Attack");
            anim.SetTrigger("AirDownAttack");
        }
    }
    private void InputCombinations() {
        if (inputs == Inputs.Square && direction == Inputs.Up) {
            Debug.Log("Up Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            anim.Play("Stab");
            anim.ResetTrigger("Attack");
        }
        if (inputs == Inputs.Square && direction == Inputs.Down) {
            Debug.Log("Down Attack!");
            if (sendInput != null) {
                sendInput("Down + Square");
            }
            ResetChain();
            anim.ResetTrigger("Attack");
            anim.Play("SwordUppercut");
            player.MoveSpeed = 0;
        }
        if (inputs == Inputs.Triangle && direction == Inputs.Down) {
            Debug.Log("Down Element!");
            if (sendInput != null) {
                sendInput("Down + Triangle");
            }
            //playerInputs.DarkPowers.TriangleDown();
            ResetChain();
            if (downTriangle != null) {
                downTriangle();
            }
        }
        if (inputs == Inputs.Triangle && direction == Inputs.Up) {
            Debug.Log("Up Element");
            if (sendInput != null) {
                sendInput("Up + Triangle");
            }
            //playerInputs.DarkPowers.TriangleUp();
            ResetChain();
            if (upTriangle != null) {
                upTriangle();
            }
        }
        if (inputs == Inputs.Circle && direction == Inputs.Up) {

            if (sendInput != null) {
                sendInput("Up + Circle");
            }
            if (upCircle != null) {
                upCircle();
            }
            playerInputs.Relic.UpCircle();
            ResetChain();
        }
        if (inputs == Inputs.Circle && direction == Inputs.Down) {

            if (sendInput != null) {
                sendInput("Down + Circle");
            }
            if (downCircle != null) {
                downCircle();
            }
            ResetChain();
        }
    }
    private void AdvancedMovement() {
        
        if (inputs == Inputs.X && direction == Inputs.Up) {
            ResetChain();
            player.CombatAnimations = 5;
            playerMove.IsJumpPressed = false;
            anim.ResetTrigger("Jump");
            anim.Play("ForwardDodge");
        }
        if (inputs == Inputs.X && direction == Inputs.Down) {
            ResetChain();
            player.CombatAnimations = 1;
            playerMove.IsJumpPressed = false;
            anim.Play("KickUp");
            anim.ResetTrigger("Jump");
        }
        if (inputs == Inputs.X && direction == Inputs.Right) {
            ResetChain();
            player.CombatAnimations = 3;
            playerMove.IsJumpPressed = false;
            print("Right dodge");
            anim.Play("RightDodge");
            anim.ResetTrigger("Jump");
        }
        if (inputs == Inputs.X && direction == Inputs.Left) {
            ResetChain();
            player.CombatAnimations = 2;
            playerMove.IsJumpPressed = false;
            print("Left dodge");
            anim.Play("LeftDodge");
            anim.ResetTrigger("Jump");
        }
    }
    private void MovementInputs() {
        if (stick.x < 0.5) {
            if (stick.y > 0)//forward
            {
                MovementChain = 1;
            }
            if (stick.y < 0)//back
            {
                MovementChain = 2;
            }
        }
        if (stick.x > 0.5)//right
        {
            MovementChain = 3;
        }
        if (stick.x < -0.5)//left
        {
            MovementChain = 4;
        }
        if (Mathf.Abs(stick.x) >= 0.001 || Mathf.Abs(stick.y) >= 0.001) {
        }
        else {
            MovementChain = 0;
        }
    }
    private void ResetMoveChain() {
        MovementChain = 0;
    }
    private void ResetChain() {
        inputs = Inputs.Null;
    }
    private void ResetDirection() {
        direction = Inputs.Null;
    }
    private void LockControl(bool val) {
        lockon = val;
    }
    private void emptyChain() {
        Chain = 0;
    }
}
