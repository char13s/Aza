using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Stand : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < 0.6f) {
            Attack1Stando.GetStando().transform.position += Attack1Stando.GetStando().transform.forward * 20 * Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }
}
