using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackBehavior : StateMachineBehaviour
{
    [SerializeField] private Vector3 knockBack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (HitBox.GetHitBox().EnemyImAttacking != null) {

        }

       
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        

    }
}
