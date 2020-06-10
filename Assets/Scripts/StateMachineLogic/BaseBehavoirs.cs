using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BaseBehavoirs : StateMachineBehaviour {

    [SerializeField] private int type;
    private Player pc;
    public static event UnityAction grounded;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (grounded != null) {
            grounded();
        }
        pc = Player.GetPlayer();
        if (type == 0) {
            if (pc.Style == 0) {
                Defaults();
            }
        }
        else {
            Defaults();
        }
    }
    private void Defaults() {
        pc.SkillId = 0;
        pc.CmdInput = 0;
        pc.MoveSpeed = 6;
        pc.SpinAttack = false;
    }

}
