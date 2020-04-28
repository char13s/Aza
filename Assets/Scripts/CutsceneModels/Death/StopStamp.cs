using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StopStamp : StateMachineBehaviour
{
    public static event UnityAction stopStamp;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (stopStamp != null) {
            stopStamp();
        }
    }
}
