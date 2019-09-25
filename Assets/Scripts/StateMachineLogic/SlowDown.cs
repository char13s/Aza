using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : StateMachineBehaviour
{
    [SerializeField] private float timeDelay;
    [SerializeField] private float timeDelayStart;
    [SerializeField] private float timeDelayEnd;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ( stateInfo.normalizedTime < timeDelayStart)
        {
            Time.timeScale = timeDelay;
        }
        if (stateInfo.normalizedTime > timeDelayEnd) { Time.timeScale = 1f; }
    }
}
