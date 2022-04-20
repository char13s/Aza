using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAttack : StateMachineBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        player.StabHitBox.SetActive(true);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff) {
            player.CharCon.Move(player.transform.forward * moveSpeed*Time.deltaTime);
            
        }
        else {
           player.StabHitBox.SetActive(false);
        }
    }
}
