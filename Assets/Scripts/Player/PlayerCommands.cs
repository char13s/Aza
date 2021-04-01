using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerCommands : MonoBehaviour {
    private enum Inputs { X, Square, Triangle, Circle, Up, Down, Right, Left, Direction }
    #region Events
    public static event UnityAction<string> sendInput;
    public static event UnityAction<int> sendChain;
    #endregion

    private Coroutine fakeUpdate;
    private bool lockon;
    #region Outside Scripts
    private Animator anim;
    private Player player;
    #endregion
    #region Anim parameters
    private int chain;
    private int movementChain;
    private Vector3 stick;
    #endregion
    [SerializeField] private List<Inputs> inputs;

    public int Chain { get => chain; set { chain = value; anim.SetInteger("ChainInput", chain); } }

    public int MovementChain { get => movementChain; set { movementChain = value; anim.SetInteger("MoveInput", movementChain); } }

    private void Awake() {
        inputs = new List<Inputs>(52);
    }
    void Start() {
        fakeUpdate = StartCoroutine(SlowUpdate());
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        ChainInput.endChain += emptyChain;
        //Player.lockOn += LockControl;
    }
    private void Update() {
        GetInputs();
    }
    private void GetInputs() {
        InputChains();
        if (player.LockedOn) {
            InputCombinations();
            MovementInputs();
            AdvancedMovement();
        }
        
        if (inputs.Count > 3) {
            ResetChain();
        }
    }
    private void FixedUpdate() {
        //AnalogInputs();
    }
    private IEnumerator SlowUpdate() {
        YieldInstruction wait = new WaitForSeconds(0.75f);
        while (isActiveAndEnabled) {
            yield return wait;
            ResetChain();
        }
    }
    private void OnMovement(InputValue value) {

        //AddInput(Inputs.Direction);
        stick = value.Get<Vector2>();
    }
    private void OnDash(InputValue value) {
        //Chain = 7;
    }
    private void OnUp() {
        AddInput(Inputs.Up);
    }
    private void OnDown() {
        AddInput(Inputs.Down);
    }
    private void OnRight() {
        AddInput(Inputs.Right);
    }
    private void OnLeft() {
        AddInput(Inputs.Left);
    }
    private void OnJump() {
    
        if (sendInput != null) {
            sendInput("X");
        }
        AddInput(Inputs.X);
    
    }
    private void OnTriangle() {

        if (sendInput != null) {
            sendInput("Triangle");
        }
        AddInput(Inputs.Triangle);

    }
    private void OnSquare(InputValue value) {

        if (sendInput != null) {
            sendInput("Square");
        }
        if (value.isPressed) {
            AddInput(Inputs.Square);
        }
    }
    private void OnCircle() {

        if (sendInput != null) {
            sendInput("Circle");
        }
        AddInput(Inputs.Circle);

    }
    private void On() {
        Chain = 16;
    }
    private void OnHoldAttack() {
        Chain = 2;
    }
    private void OnHoldEnergy() {
        Chain = 17;
    }
    private void AddInput(Inputs button) {
        inputs.Add(button);
    }
    private void InputChains() {

        if (inputs.Contains(Inputs.Triangle) && inputs.Contains(Inputs.Circle)) {
            Debug.Log("Fire!BIcth");
            Chain = 9;
            ResetChain();
        }
        if (inputs.Contains(Inputs.X)) {
            Chain = 1;
        }
        /*if (inputs.Contains(Inputs.Square)) {
            Chain = 2;
            
        }
        if (inputs.Contains(Inputs.Triangle)) {
            Chain = 3;

        }
        if (inputs.Contains(Inputs.Circle)) {
            Chain = 4;
        }
        if (inputs.Contains(Inputs.Circle) && inputs.Contains(Inputs.Direction)) {

            Chain = 7;
        }*/
    }
    private void InputCombinations() {
        if (inputs.Contains(Inputs.Square) && inputs.Contains(Inputs.Up)) {
            Debug.Log("Up Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            Chain = 3;
        }
        if (inputs.Contains(Inputs.Square) && inputs.Contains(Inputs.Down)) {
            Debug.Log("Down Attack!");
            if (sendInput != null) {
                sendInput("Down + Square");
            }
            Chain = 4;
            ResetChain();
            //Insert Chain Here.
        }
        if (inputs.Contains(Inputs.Circle) && inputs.Contains(Inputs.Up)) {

            if (sendInput != null) {
                sendInput("Up + Circle");
            }
            Chain = 6;
            ResetChain();
        }
    }
    private void AdvancedMovement() {
        if (inputs.Contains(Inputs.X) && inputs.Contains(Inputs.Up)) {
            ResetChain();
            player.CombatAnimations = 5;
        }
        if (inputs.Contains(Inputs.X) && inputs.Contains(Inputs.Down)) {
            ResetChain();
            player.CombatAnimations = 6;
        }
        if (inputs.Contains(Inputs.X) && inputs.Contains(Inputs.Right)) {
            ResetChain();
            player.CombatAnimations = 7;
        }
        if (inputs.Contains(Inputs.X) && inputs.Contains(Inputs.Left)) {
            ResetChain();
            player.CombatAnimations = 8;
        }
    }
    private void MovementInputs() {
        if (stick.x == 0) {
            if (stick.y > 0)//forward
            {
                MovementChain = 1;

            }

            if (stick.y < 0)//back
            {

                MovementChain = 2;
            }
        }

        if (stick.x > 0.3)//right
        {
            MovementChain = 3;
            Debug.Log("right");
        }

        if (stick.x < -0.3)//left
        {
            MovementChain = 4;

        }
        if (Mathf.Abs(stick.x) >= 0.001 || Mathf.Abs(stick.y) >= 0.001) {

            
        }
        else {

            MovementChain = 0;
        }
    }
    private void ResetChain() {
        inputs.Clear();
    }
    private void LockControl(bool val) {
        lockon = val;
    }
    private void emptyChain() {
        Chain = 0;
    }
}
