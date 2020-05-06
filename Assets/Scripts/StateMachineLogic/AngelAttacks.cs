using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttacks : StateMachineBehaviour
{
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.FakeAngelSword.SetActive(false);
        pc.AngelSword.SetActive(true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.AngelSword.SetActive(false);
        pc.FakeAngelSword.SetActive(true);
    }

}
