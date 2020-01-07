using UnityEngine;

public class Dodge : StateMachineBehaviour
{
    private float regularSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        regularSpeed = Player.GetPlayer().MoveSpeed;
        Player.GetPlayer().MoveSpeed = 10;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().MoveSpeed =regularSpeed;
    }
}
