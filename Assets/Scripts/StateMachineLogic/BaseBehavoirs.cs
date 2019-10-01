using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavoirs : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().SkillId = 0;
        Player.GetPlayer().CmdInput = 0;
        Player.GetPlayer().MoveSpeed = 5;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
