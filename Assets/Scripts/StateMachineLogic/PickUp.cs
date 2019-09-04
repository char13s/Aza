using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.5)
        {
            
            Player.GetPlayer().PickUp1 = false;
        }
    }
}
