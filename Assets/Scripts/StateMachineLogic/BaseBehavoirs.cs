using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavoirs : StateMachineBehaviour
{
    private Player pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        pc.SkillId = 0;
        pc.CmdInput = 0;
       pc.MoveSpeed = 6;
        pc.SpinAttack = false;
        //pc.Anim.SetLayerWeight(3,1);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (Input.GetButton("Square")) {

        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
