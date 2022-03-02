using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AttackStates : StateMachineBehaviour
{
    public static event UnityAction sendAttack;
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        sendAttack.Invoke();
    }
}
