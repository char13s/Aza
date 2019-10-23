using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : StateMachineBehaviour
{
    private float firstSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        firstSpeed = Player.GetPlayer().MoveSpeed;
        Player.GetPlayer().MoveSpeed = 0;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Player.GetPlayer().MoveSpeed = firstSpeed;
    }
}
