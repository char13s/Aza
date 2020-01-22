using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrow : StateMachineBehaviour
{
    [SerializeField] private GameObject sword;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Instantiate(sword, Player.GetPlayer().RightHand.transform.position, Quaternion.identity);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.normalizedTime==0.5f)
        {
            
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Player.GetPlayer().TeleportTriggered = false;
        Player.GetPlayer().Cinemations = 0;
    }
}
