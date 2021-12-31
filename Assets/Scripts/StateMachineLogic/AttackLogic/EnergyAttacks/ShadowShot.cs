using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ShadowShot : StateMachineBehaviour
{

    public static event UnityAction shoot;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        shoot.Invoke();
    }
}
