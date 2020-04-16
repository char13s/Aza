using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCombos : StateMachineBehaviour
{
    private Player pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        pc.RBody.drag = 125;
        pc.RBody.useGravity = true;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       pc.RBody.drag = 125;

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        pc.RBody.drag = 0;

    }
}
