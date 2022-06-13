using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFlip : StateMachineBehaviour
{
    [SerializeField] private float move;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //Player.GetPlayer().MoveSpeed = 0;
        //Player.GetPlayer().Nav.enabled = false;
        //.GetPlayer().RBody.isKinematic = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Player.GetPlayer().CharCon.Move(-move * Time.deltaTime * Player.GetPlayer().transform.forward);
        //Player.GetPlayer().Rbody.AddForce(Player.GetPlayer().transform.forward * -move, ForceMode.Impulse);
        //Player.GetPlayer().transform.position -= Player.GetPlayer().transform.forward * move * Time.deltaTime;
        //Player.GetPlayer().RBody.velocity = Player.GetPlayer().transform.forward * -move;
        // Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward * -move,ForceMode.Impulse);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Player.GetPlayer().CombatAnimations = 0;
        Player.GetPlayer().GuardAnimations = 0;
    }
}
