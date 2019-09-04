using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ( stateInfo.normalizedTime < 0.95f)
        {
            Time.timeScale = 0.2f;
        }
        if (stateInfo.normalizedTime > 1f) { Time.timeScale = 1f; }
    }
}
