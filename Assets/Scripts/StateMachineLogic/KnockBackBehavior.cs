using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackBehavior : StateMachineBehaviour
{
    // private Vector3 knockBack;
    [SerializeField]private int attackId;
    
    private static int hitId;
    private static int animationId;

    public static int HitId { get => hitId; set => hitId = value; }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         HitId= attackId;
        /*if (HitBox.GetHitBox().EnemyImAttacking != null) {

        }*/
    }

    
}
