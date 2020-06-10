using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    [SerializeField] private float move;
    [SerializeField] private GameObject burst;
    private Player pc;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        pc.Grounded = false;
        pc.CantDoubleJump = false;
        //pc.RBody.velocity = new Vector3(0, 0, 0);
        pc.RBody.AddForce(pc.transform.forward * 120, ForceMode.Impulse);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //pc.transform.position = Vector3.MoveTowards(pc.transform.position,pc.HitPoint.transform.position,move*Time.deltaTime); 
       //pc.transform.position = Vector3.MoveTowards(pc.transform.position, pc.JumpPoint.transform.position, move * Time.deltaTime);
        pc.Grounded = false;
        //pc.RBody.AddForce();
        //Player.GetPlayer().GroundChecker.SetActive(false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.GetPlayer().GroundChecker.SetActive(true);
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
