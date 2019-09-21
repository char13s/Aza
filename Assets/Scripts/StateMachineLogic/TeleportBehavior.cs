using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : StateMachineBehaviour
{
    [SerializeField]private float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetPlayer().BattleMode.EnemyTarget != null)
        {
            Player.GetPlayer().transform.position = Player.GetPlayer().BattleMode.EnemyTarget.transform.position;
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime == time && Player.GetPlayer().BattleMode.EnemyTarget != null)
        {

            Player.GetPlayer().transform.position = Player.GetPlayer().BattleMode.EnemyTarget.transform.position;


        }
    }
}
