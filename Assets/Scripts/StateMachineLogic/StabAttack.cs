using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAttack : StateMachineBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    private bool stopped;
    Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        StabEnder.stop += Stop;
        stopped = false;
        //player.StabHitBox.SetActive(true);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff&&!stopped) {
            player.CharCon.Move(player.transform.forward * moveSpeed * Time.deltaTime);

        }
        else {
            //player.StabHitBox.SetActive(false);
        }
        if (stopped) {
            animator.Play("Stab");
        }
    }
    private void Stop() {
        stopped = true;
        
    } 
}