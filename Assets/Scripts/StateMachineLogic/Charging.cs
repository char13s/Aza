using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private GameObject sparks;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.Charging = true;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if(animatorStateInfo.normalizedTime>0.1f&& animatorStateInfo.normalizedTime < 0.9f)
        Instantiate(sparks, pc.transform);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.Charging = false;
    }
}
