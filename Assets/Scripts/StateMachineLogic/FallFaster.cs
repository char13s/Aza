using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFaster : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private float fallRate;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (animatorStateInfo.normalizedTime < 0.9f)
            pc.Rbody.AddForce(pc.transform.up*-2,ForceMode.VelocityChange);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        
    }
}
