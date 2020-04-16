using UnityEngine;
using UnityEngine.Events;
public class DoubleJump : StateMachineBehaviour
{
    [SerializeField] private float move;
    [SerializeField] private GameObject burst;

    private AudioClip sound;
    public static event UnityAction<AudioClip> doubleJump;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sound = AudioManager.GetAudio().DoubleJump;
        Instantiate(burst, Player.GetPlayer().transform.position, Quaternion.identity);
        if (doubleJump != null) {
            doubleJump(sound);
        }
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().transform.position = Vector3.MoveTowards(Player.GetPlayer().transform.position, Player.GetPlayer().JumpPoint.transform.position, move * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        Player.GetPlayer().SecondJump = false;
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
