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
	private Player player;
	private PlayerTimelineControl timelines;
	private PlayerInputs playerInputs;
	#endregion
	#region Anim parameters
	private int chain;
	private int movementChain;
	private Vector2 stick;
	#endregion
	[SerializeField] private Inputs inputs;
	[SerializeField] private Inputs direction;
	public int Chain { get => chain; set { chain = value; anim.SetInteger("ChainInput", chain); } }

	public int MovementChain { get => movementChain; set { movementChain = value; anim.SetInteger("MoveInput", movementChain); } }

	private void Awake() {
		//inputs = new List<Inputs>(52);

	}
	private void OnEnable() {
		//fakeUpdate=StartCoroutine(SlowUpdate());
	}
	private void OnDisable() {
		//StopCoroutine(SlowUpdate());
	}
	void Start() {
		player = GetComponent<Player>();
		anim = player.Anim;
		timelines = GetComponent<PlayerTimelineControl>();
		playerInputs = GetComponent<PlayerInputs>();
		BaseBehavoirs.baseB += ResetMoveChain;
		ChainInput.resetChain += emptyChain;
		//ChainInput.endChain += emptyChain;
		//Player.lockOn += LockControl;
	}
	private void Update() {
		//if (!player.SkillButton) {
			GetInputs();
			if (stick.sqrMagnitude == 0) {
				ResetDirection();
			}
		//}
	}
	private void GetInputs() {
		InputChains();
		if (player.LockedOn) {
			InputCombinations();
			AdvancedMovement();
			if (player.CombatAnimations == 0) {
				MovementInputs();
			}
		}
	}
	private IEnumerator SlowUpdate() {

		YieldInstruction wait = new WaitForSeconds(0.5f);
		while (isActiveAndEnabled) {
			yield return wait;
			ResetChain();
		}
	}

	private void OnMovement(InputValue value) {

		//AddInput(Inputs.Direction);
		stick = value.Get<Vector2>();
		Vector2 min = new Vector2(0.01f, 0.01f);
		/*if (stick.SqrMagnitude() < min.SqrMagnitude()) {
            ResetDirection();
        }*/
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
	private void OnJump() {

		if (sendInput != null) {
			sendInput("X");
		}
		if (player.CombatAnimations == 0) {
			anim.SetTrigger("Jump");
		}

		AddInput(Inputs.X);
	}
	private void OnEnergy() {
		if (sendInput != null) {
			sendInput("Triangle");
		}
		AddInput(Inputs.Triangle);
		if (triangle != null) {
			triangle();
		}
	}
	private void OnAttack(InputValue value) {

		if (sendInput != null) {
			sendInput("Square");
		}
		if (value.isPressed) {
			AddInput(Inputs.Square);
		}
	}
	private void OnAbility() {

		if (sendInput != null) {
			sendInput("Circle");
		}
		AddInput(Inputs.Circle);
		if (circle != null) {
			circle();
		}
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
		timelines.PlayHoldAttack();
	}
	private void OnHoldEnergy() {
		Chain = 17;
	}
	private void OnHoldTriangle(InputValue value) {
		//if (value.isPressed) {
		//    //charge fire ball 
		//}
		//else {
		//    //SHOOT FIRE
		//}
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
		if (inputs == Inputs.Square && direction == Inputs.Up) {
			Debug.Log("Up Attack!");
			if (sendInput != null) {
				sendInput("Up + Square");
			}
			ResetChain();
			//Chain = 3;
			//timelines.PlayUpAttack();
			anim.SetTrigger("UpAttack");
		}
		if (inputs == Inputs.Square && direction == Inputs.Down) {
			Debug.Log("Down Attack!");
			if (sendInput != null) {
				sendInput("Down + Square");
			}
			//Chain = 4;
			ResetChain();
			//timelines.PlayDownAttack();
			anim.ResetTrigger("Attack");
			anim.SetTrigger("DownAttack");
			
			//Insert Chain Here.
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
			//timelines.PlayDownEnergy();
			//Insert Chain Here.
		}
		if (inputs == Inputs.Triangle && direction == Inputs.Up) {
			Debug.Log("Up Element");
			if (sendInput != null) {
				sendInput("Up + Triangle");
			}
			//playerInputs.DarkPowers.TriangleUp();
			ResetChain();
			//anim.SetTrigger("Slash");
			if (upTriangle != null) {
				upTriangle();
			}
			//timelines.PlayDownEnergy();
			//Insert Chain Here.
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
			//playerInputs.Relic.DownCircle();
			ResetChain();
		}
		/*if (inputs.Contains(Inputs.Up) && inputs.Contains(Inputs.Down)&&inputs.Contains(Inputs.Square)) {
            Debug.Log("Up + Down Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            //Chain = 3;
        }*/
	}
	private void AdvancedMovement() {
		if (inputs == Inputs.X && direction == Inputs.Up) {
			ResetChain();
			player.CombatAnimations = 5;
			anim.ResetTrigger("Jump");
		}
		if (inputs == Inputs.X && direction == Inputs.Down) {
			ResetChain();
			player.CombatAnimations = 1;
			anim.ResetTrigger("Jump");
		}
		if (inputs == Inputs.X && direction == Inputs.Right) {
			ResetChain();
			player.CombatAnimations = 3;
			print("Right dodge");
			anim.ResetTrigger("Jump");
		}
		if (inputs == Inputs.X && direction == Inputs.Left) {
			ResetChain();
			player.CombatAnimations = 2;
			print("Left dodge");
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
