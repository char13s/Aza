using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MovingStates : StateMachineBehaviour
{
    private Player pc;
    private GameManager manager;
    [SerializeField] private float speedOfState;
    [SerializeField] private Vector3 direction;
    [SerializeField] bool isFalling;
    [SerializeField] bool singularDirection;
    public static event UnityAction<float> returnSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        manager = GameManager.GetManager();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (manager.CurrentState == GameManager.GameState.PlayMode) {
            returnSpeed.Invoke(speedOfState);
            if (isFalling) {
                pc.CharCon.Move(speedOfState * direction * Time.deltaTime);

            }
            else {
                pc.CharCon.Move(speedOfState * pc.PlayerMove.Direction * Time.deltaTime);
                pc.CharCon.Move(-0.5f * pc.transform.up * Time.deltaTime);
            }
        }
        //pc.MoveSpeed = speedOfState;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //returnSpeed.Invoke(0);
    }

}
