using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehavior : StateMachineBehaviour
{
    private GameObject fireTrail;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireTrail = Player.GetPlayer().FireTrail;
        
        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.3) { fireTrail.SetActive(true); }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireTrail.SetActive(false);
    }
}
