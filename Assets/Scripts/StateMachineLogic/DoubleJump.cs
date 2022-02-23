using UnityEngine;
using UnityEngine.Events;
public class DoubleJump : StateMachineBehaviour
{
    [SerializeField] private float move;
    [SerializeField] private GameObject burst;
    private Player pc;
    private AudioClip sound;
    public static event UnityAction<AudioClip> doubleJump;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        sound = AudioManager.GetAudio().DoubleJump;
        Instantiate(burst, Player.GetPlayer().transform.position, Quaternion.identity);
        if (doubleJump != null) {
            doubleJump(sound);
        }
        pc.RBody.velocity = new Vector3(0, 0, 0);
        //pc.RBody.AddForce(pc.transform.forward * 120, ForceMode.Impulse);
        pc.RBody.AddForce(new Vector3(0, move, 0), ForceMode.Impulse);
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //pc.transform.position = Vector3.MoveTowards(pc.transform.position, pc.HitPoint.transform.position, (move/6) * Time.deltaTime);
    //    //pc.transform.position = Vector3.MoveTowards(pc.transform.position, pc.JumpPoint.transform.position, move * Time.deltaTime);
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        pc.SecondJump = false;
   }

}
