using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KillOtherLayers : StateMachineBehaviour
{
    public static event UnityAction<int> weight;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (weight != null) {
            weight(1);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (weight != null) {
            weight(0);
        }
    }
}
