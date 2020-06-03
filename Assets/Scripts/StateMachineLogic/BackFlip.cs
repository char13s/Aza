using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFlip : StateMachineBehaviour
{
    [SerializeField] private float move;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Player.GetPlayer().MoveSpeed = 0;
        //Player.GetPlayer().Nav.enabled = false;
        Player.GetPlayer().RBody.isKinematic = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Player.GetPlayer().transform.position -= Player.GetPlayer().transform.forward * move * Time.deltaTime;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Player.GetPlayer().CombatAnimations = 0;
        Player.GetPlayer().GuardAnimations = 0;
    }
}
