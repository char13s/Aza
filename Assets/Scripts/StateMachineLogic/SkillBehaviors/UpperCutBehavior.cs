using UnityEngine;

public class UpperCutBehavior : StateMachineBehaviour
{

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player.GetPlayer().HitBox.SetActive(true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().HitBox.SetActive(false);
    }
}
