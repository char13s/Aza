using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MovingStates : StateMachineBehaviour
{
    private Player player;
    [SerializeField] private float speedOfState;
    [SerializeField]private float move;
    private Vector3 speed;
    public static event UnityAction<float> returnSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        if (player.Anim.GetLayerWeight(layerIndex) == 1) {
            player.MoveSpeed = speedOfState;
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //if (layerIndex==0 && pc.Anim.GetLayerWeight(2) == 0) {
        //    pc.MoveSpeed = speedOfState;
        //}
        speed = move * player.Direction.normalized;
        speed.y = player.Rbody.velocity.y;
        player.Rbody.velocity = speed;
    }
}
