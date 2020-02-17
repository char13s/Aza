using UnityEngine;

public class Dodge : StateMachineBehaviour
{
    private float regularSpeed;
    private Player pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
       //regularSpeed = Player.GetPlayer().MoveSpeed;
       //Player.GetPlayer().MoveSpeed = 10;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
            
            Debug.Log("Boosting");
            pc.CmdInput = 0;
            pc.RBody.isKinematic = false;
            pc.RBody.AddForce(pc.transform.forward * pc.BurstForce, ForceMode.VelocityChange);
            if (pc.RBody.isKinematic) {
                Debug.Log("wtf is good?");
            }
            //MoveSpeed = 13;
            
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.GetPlayer().MoveSpeed =regularSpeed;
    }
}
