using UnityEngine;
using UnityEngine.Events;
public class UpperCutBehavior : StateMachineBehaviour
{
    [SerializeField] private float move;
    private bool goingUp;
    private Player pc;

    public bool GoingUp { get => goingUp; set { goingUp = value;if (sendGoingUp != null) { sendGoingUp(); } } }

    public static event UnityAction sendGoingUp;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (stateInfo.normalizedTime > 0.99f && Input.GetButton("Triangle")&&!GoingUp) {
            GoingUp = true;
        }
        if (Input.GetButtonUp("Triangle")) {
            GoingUp = false;
        }
        
        if (stateInfo.normalizedTime > 0.99f&&GoingUp) {
            //pc.transform.position = Vector3.MoveTowards(pc.transform.position, pc.HighPoint.transform.position, move * Time.deltaTime);
            //pc.RBody.useGravity = false;
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //pc.RBody.useGravity = false;
        GoingUp = false;
    }
}
