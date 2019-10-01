using UnityEngine;

public class CommandInputBehavior : StateMachineBehaviour
{
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().CmdInput = 0;
        Player.GetPlayer().MoveSpeed = 0;
        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetInput();
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff)
        {
            Player.GetPlayer().HitBox.SetActive(true);
        }
        else
        {
            Player.GetPlayer().HitBox.SetActive(false);
        }

    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    private void HitBoxControl() {
        
    }
    private void GetInput() {
        if (Input.GetButtonDown("Square"))
        {
            Player.GetPlayer().CmdInput = 1;
        }

        if (Input.GetButtonDown("Triangle"))
        {
            Player.GetPlayer().CmdInput = 2;
        }

    }

}
