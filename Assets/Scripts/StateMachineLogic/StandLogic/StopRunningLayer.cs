using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRunningLayer : StateMachineBehaviour
{
    private Player pc;
    private int legLayerIndex;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        //legLayerIndex = pc.LegsLayer;
        pc.Anim.SetLayerWeight(legLayerIndex, 0);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.Anim.SetLayerWeight(legLayerIndex, 0);
        //Debug.Log("FUCKKKKKKK");
        //if (animatorStateInfo.normalizedTime > 0.9f) {
        //    pc.Anim.SetLayerWeight(3, 1);
        //}
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.Anim.SetLayerWeight(legLayerIndex, 1);
    }
}
