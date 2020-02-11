using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateMachineBehaviour
{
    [SerializeField] private GameObject burst;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().LeftHand.SetActive(true);
        Player.GetPlayer().RightHand.SetActive(true);
        //Player.GetPlayer().DevilFoot.SetActive(true);
        Instantiate(burst, Player.GetPlayer().transform.position, Quaternion.identity);
        //burst.transform.position=
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Player.GetPlayer().RBody.drag = 0;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().LeftHand.SetActive(false);
        Player.GetPlayer().RightHand.SetActive(false);
        Player.GetPlayer().DevilFoot.SetActive(false);
    }
}
