using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavoirs : StateMachineBehaviour
{
    private GameObject axe;
    [SerializeField] private Items wood;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetPlayer().Chopping)
        {
            axe = Player.GetPlayer().Axe;
            axe.SetActive(true);

        }

        Player.GetPlayer().Chopping = false;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetPlayer().Chopping && stateInfo.normalizedTime > 0.5f)
        {
            axe.SetActive(false);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        axe.SetActive(false);
        Player.GetPlayer().items.AddItem(wood.data);


    }
}
