using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSkillBehavoir : StateMachineBehaviour
{
    private GameObject forwardHitbox;
    private GameObject hitbox;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (Player.GetPlayer().SkillId)
        {
            case 6:
                forwardHitbox = Player.GetPlayer().ForwardHitbox;
                forwardHitbox.SetActive(true);

                break;
            case 7:
                Debug.Log("Hit box should be on");
                hitbox = Player.GetPlayer().HitBox;
                hitbox.SetActive(true);
                break;
        }

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float time = 0.9f;
        if (stateInfo.normalizedTime > time)
        {
            switch (Player.GetPlayer().SkillId)
            {
                case 6:
                    forwardHitbox.SetActive(false);
                    break;
                case 7:
                    Debug.Log("Hit box should be off");
                    hitbox.SetActive(false);
                    break;
            }
        }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
