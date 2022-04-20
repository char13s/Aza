using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class AttackingBehavior : StateMachineBehaviour
{
    [SerializeField]private bool hasNext;
    [SerializeField]private string nextAttack;
    private Player player;
    //[SerializeField] private GameObject slash;
    private bool pressed;
    private bool shoot;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Player.GetPlayer();
        player.Attack=false;
        pressed = false;
        shoot = false;
        Debug.Log("STate Entered");
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.Attack) {
            pressed = true;
        }

        if (stateInfo.normalizedTime >= 0.9f) {
  
            if (pressed && hasNext) {
                animator.Play(nextAttack);
            }
            else if (shoot) {
                animator.Play("ShadowShot");
            }
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (pressed && hasNext) {
            animator.Play(nextAttack);
        }
        else if (shoot) {
            animator.Play("ShadowShot");
        }


    }

}
