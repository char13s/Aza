using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackBehavior : StateMachineBehaviour
{
    //[SerializeField] private Vector3 knockBack;
    private static int animationId;

    public static int AnimationId { get => animationId; set => animationId = value; }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimationId = Player.GetPlayer().HitCounter;
        /*if (HitBox.GetHitBox().EnemyImAttacking != null) {

        }*/
    }
}
