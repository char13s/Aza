using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCombos : StateMachineBehaviour
{
    public enum AirCombo { 
     combo1, combo2,combo3
    }
    [SerializeField] private AirCombo airCombo;
    [SerializeField] private int comboCount;
    private Player pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        pc.RBody.drag = 125;
        pc.RBody.useGravity = false;
        pc.AirCombo = comboCount;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       pc.RBody.drag = 125;

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.RBody.useGravity = true;
        pc.RBody.drag = 0;

    }
}
