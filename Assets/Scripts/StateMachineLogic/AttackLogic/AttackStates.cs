using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AttackStates : StateMachineBehaviour
{
    [SerializeField] private float knockPower;
    public static event UnityAction<float> sendKnockPower;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //if (sendKnockPower != null) {
        //    sendKnockPower(0);
        //}

    }
}
