using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBolt : StateMachineBehaviour
{
    [SerializeField] private GameObject bolt;
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Instantiate(bolt, pc.BattleCamTarget.transform);
    }
}
