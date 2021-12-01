using UnityEngine;
using UnityEngine.Events;
public class Dodge : StateMachineBehaviour
{
    [SerializeField] private float move;
    public static event UnityAction stopMove;
    public static event UnityAction resetMove;
    public static event UnityAction dodge;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.GetPlayer().MoveSpeed = 0;
        //Player.GetPlayer().Nav.enabled = false;
        //Player.GetPlayer().RBody.isKinematic = false;
        Player.GetPlayer().CombatAnimations = 0;
        //if (stopMove != null) {
        //    stopMove();
        //}
        //regularSpeed = Player.GetPlayer().MoveSpeed;
        //Player.GetPlayer().MoveSpeed = 10;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Player.GetPlayer().RBody.velocity = Player.GetPlayer().transform.right * move;
        //MoveSpeed = 13;  
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stopMove != null) {
        //    resetMove();
        //}
        //Player.GetPlayer().MoveSpeed =regularSpeed;
    }
}
