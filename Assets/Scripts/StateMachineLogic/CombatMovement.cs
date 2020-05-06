using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMovement : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private float speed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.MoveSpeed = speed;
    }
}
