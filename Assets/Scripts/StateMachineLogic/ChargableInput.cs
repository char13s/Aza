using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableInput : StateMachineBehaviour
{
    private float chargeMeter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        chargeMeter = 0;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Input.GetButton("Square")) {
            chargeMeter += 1;
        }
    }
}
