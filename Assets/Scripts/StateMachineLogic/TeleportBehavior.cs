using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : StateMachineBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Vector3 teleportEndPoint;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (Player.GetPlayer().BattleMode.EnemyTarget != null)
        {
            Player.GetPlayer().transform.position = Player.GetPlayer().BattleMode.EnemyTarget.transform.position;
        }

        
    }
    /*override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {float distance = Vector3.Distance(Player.GetPlayer().transform.position, Player.GetPlayer().BattleMode.EnemyTarget.transform.position);

        if (Player.GetPlayer().BattleMode.EnemyTarget != null)
        {
            if (stateInfo.normalizedTime => time)
            {

                
            }

        }
        




    }*/
}
