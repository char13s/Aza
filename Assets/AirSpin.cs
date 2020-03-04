using UnityEngine;
using XInputDotNetPure;
using UnityEngine.Events;
public class AirSpin : StateMachineBehaviour
{
    [SerializeField] private GameObject reminant;
    public static event UnityAction destroyReminants;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().HitBox.SetActive(true);
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GamePad.SetVibration(0,0.4f,0.4f);
        if (stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < 0.8f) { 
            if (reminant != null) {
                Instantiate(reminant, Player.GetPlayer().transform);
            }
            //Player.GetPlayer().ForwardHitbox.SetActive(true);
        }
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (destroyReminants != null) {
            destroyReminants();
        }
        Player.GetPlayer().HitBox.SetActive(false);
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
