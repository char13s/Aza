using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MovingStates : StateMachineBehaviour
{
    private Player pc;
    [SerializeField]private float speedOfState;
    [SerializeField] private Vector3 direction;
    public static event UnityAction<float> returnSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        returnSpeed.Invoke(speedOfState);
        pc.CharCon.Move(speedOfState* direction*Time.deltaTime);
        //pc.MoveSpeed = speedOfState;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //returnSpeed.Invoke(0);
    }
    
}
