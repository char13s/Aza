using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KnockAwayHitBox : StateMachineBehaviour
{
    public static event UnityAction<bool> hitBox;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        hitBox.Invoke(true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        hitBox.Invoke(false);
    }
}
