using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChainInput : StateMachineBehaviour
{
    public static event UnityAction resetChain;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        resetChain.Invoke();
    }
}
