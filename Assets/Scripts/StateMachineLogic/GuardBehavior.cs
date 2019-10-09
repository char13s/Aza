using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehavior : StateMachineBehaviour
{
    [SerializeField] private float ShieldUp;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > ShieldUp)
        {

        }
    }
}
