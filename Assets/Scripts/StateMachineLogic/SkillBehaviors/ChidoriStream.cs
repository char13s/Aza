using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChidoriStream : StateMachineBehaviour
{
    [SerializeField] private GameObject chidoriStream;
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        Instantiate(chidoriStream,pc.transform);
        pc.CmdInput = 0;
    }
}
