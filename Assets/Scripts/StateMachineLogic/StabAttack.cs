using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAttack : StateMachineBehaviour
{
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
         Player.GetPlayer().StabHitBox.SetActive(true);

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff) {
           
            
        }
        else {
            Player.GetPlayer().StabHitBox.SetActive(false);
        }
    }
}
