using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehaviour : StateMachineBehaviour
{
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff)
        {
            Player.GetPlayer().HitBox.SetActive(true);
        }
        else
        {
            Player.GetPlayer().HitBox.SetActive(false);
        }
    }
}
