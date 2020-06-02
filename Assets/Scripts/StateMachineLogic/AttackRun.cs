using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRun : StateMachineBehaviour
{
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        //pc.MoveSpeed = 6;
    }
}
