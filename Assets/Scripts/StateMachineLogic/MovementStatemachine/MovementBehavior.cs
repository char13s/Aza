using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : StateMachineBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float move;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Player.GetPlayer().CharCon.Move(direction*move * Time.deltaTime);
    }

}
