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
        pc.AirCombo = comboCount;
        pc.AirAttack = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // pc.AirAttack = false;
    }
}
