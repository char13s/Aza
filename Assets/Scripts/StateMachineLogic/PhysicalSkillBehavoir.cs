using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSkillBehavoir : StateMachineBehaviour
{
    private GameObject forwardHitbox;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        forwardHitbox = Player.GetPlayer().ForwardHitbox;
        forwardHitbox.SetActive(true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        forwardHitbox.SetActive(false);
    }
}
