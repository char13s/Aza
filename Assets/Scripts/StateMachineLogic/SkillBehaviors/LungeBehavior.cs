using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungeBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().SkillId = 9;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.3) {
            if (Player.GetPlayer().BattleMode.EnemyTarget != null)
            {
                Vector3 delta = Player.GetPlayer().BattleMode.EnemyTarget.transform.position - Player.GetPlayer().transform.position;
                delta.y = 0;
                Player.GetPlayer().transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            }
        }
        Player.GetPlayer().Move(13);
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Player.GetPlayer().SkillId = 0;
    }
}
