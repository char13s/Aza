using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSkillBehavoir : StateMachineBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    Player player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Player.GetPlayer();

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff) {
            player.CharCon.Move(moveSpeed * Time.deltaTime * direction);

        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }
}
