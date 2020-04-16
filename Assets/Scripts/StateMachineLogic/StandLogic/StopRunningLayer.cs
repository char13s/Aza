using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRunningLayer : StateMachineBehaviour
{
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.Anim.SetLayerWeight(3, 0);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.Anim.SetLayerWeight(3, 0);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.Anim.SetLayerWeight(3, 1);
    }
}
