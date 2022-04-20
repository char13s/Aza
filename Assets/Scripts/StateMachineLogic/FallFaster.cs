using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFaster : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private float fallRate;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        
        //pc.RBody.AddForce(pc.transform.up*-fallRate,ForceMode.Impulse);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
            pc.CharCon.Move(pc.transform.up * -fallRate*Time.deltaTime);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        
    }
}
