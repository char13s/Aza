using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class JudgementCut : StateMachineBehaviour
{
    private bool gone;
    [SerializeField] private GameObject poof;
    [SerializeField] private GameObject cut;
    public static event UnityAction stop;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gone = false;
        if(stop!=null)
        {
            stop();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.5f&&!gone) {
            Instantiate(poof,Player.GetPlayer().transform);
            Player.GetPlayer().Body.SetActive(false);
            gone = true;
            Debug.Log("Nigga Gone!");
        }
        if (stateInfo.normalizedTime > 0.9f) {
            Instantiate(cut, Player.GetPlayer().transform);
            Player.GetPlayer().RapidHitBox.SetActive(true);
            Debug.Log("ouch");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(poof, Player.GetPlayer().transform);
        Player.GetPlayer().Body.SetActive(true);
        Player.GetPlayer().RapidHitBox.SetActive(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
