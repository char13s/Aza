using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AttackStates : StateMachineBehaviour
{
    public static event UnityAction sendAttack;
    [SerializeField] private float move;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //Player.GetPlayer().MoveSpeed = 0;
        //Player.GetPlayer().RBody.isKinematic = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //Player.GetPlayer().transform.position += Player.GetPlayer().transform.forward * move * Time.deltaTime;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        sendAttack.Invoke();
    }
}
