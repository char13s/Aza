using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BaseBehavoirs : StateMachineBehaviour
{

    [SerializeField] private int type;
    private Player pc;
    public static event UnityAction grounded;
    public static event UnityAction baseB;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (grounded != null) {
            grounded();
        }
        baseB.Invoke();
        pc = Player.GetPlayer();
        Defaults();
        animator.ResetTrigger("HoldAttack");
        animator.ResetTrigger("Attack");

    }
    private void Defaults() {
        pc.SkillId = 0;
        pc.CmdInput = 0;
        //pc.MoveSpeed = 6;
        pc.AirCombo = 0;
       
    }

}
