using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WithdrawSword : StateMachineBehaviour
{
    
    public static event UnityAction withdraw;
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {

        if (withdraw != null) {
            withdraw();
        }
    }
}
