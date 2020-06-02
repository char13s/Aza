using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavoirs : StateMachineBehaviour {

    [SerializeField] private int type;
    private Player pc;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        if (type == 0) {
            if (pc.Style == 0) {
                Defaults();
            }
            


        }else {
                Defaults();
            }

        //pc.Anim.SetLayerWeight(3,1);
    }
    private void Defaults() {
        pc.SkillId = 0;
        pc.CmdInput = 0;
        pc.MoveSpeed = 6;
        pc.SpinAttack = false;
    }

}
