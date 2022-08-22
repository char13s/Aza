using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehavior : StateMachineBehaviour
{
    Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = Player.GetPlayer();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player.CharCon.SimpleMove(Player.GetPlayer().Body.transform.forward * 15);
    }
}
