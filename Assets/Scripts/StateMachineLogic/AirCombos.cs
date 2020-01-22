using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCombos : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Player.GetPlayer().RBody.drag = 125;

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player.GetPlayer().RBody.drag = 125;

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player.GetPlayer().RBody.drag = 0;

    }
}
