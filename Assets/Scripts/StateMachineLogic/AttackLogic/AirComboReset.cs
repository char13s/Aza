using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirComboReset : StateMachineBehaviour
{
    private Player pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        pc = Player.GetPlayer();
            Defaults();
    }
    private void Defaults() {
        pc.AirCombo = 0;
        pc.AirAttack = false;
    }
}
