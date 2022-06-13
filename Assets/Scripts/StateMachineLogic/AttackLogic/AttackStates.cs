using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AttackStates : StateMachineBehaviour
{
    public static event UnityAction sendAttack;
    private Player player;
    [SerializeField] private float move;
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //Player.GetPlayer().MoveSpeed = 0;
        //Player.GetPlayer().RBody.isKinematic = false;
        player=Player.GetPlayer();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Player.GetPlayer().transform.position += Player.GetPlayer().transform.forward * move * Time.deltaTime;
        
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff) {
           player.CharCon.Move(move * Time.deltaTime * player.transform.forward);

        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        sendAttack.Invoke();
    }
}
