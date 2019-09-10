using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class Slam : StateMachineBehaviour
{
    [SerializeField] private GameObject boom;
    private GameObject AoeHitbox;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AoeHitbox = Player.GetPlayer().AoeHitbox1;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (stateInfo.normalizedTime > 0.9f)
        {
            AoeHitbox.SetActive(true);
            Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        AoeHitbox.SetActive(false);
    }
}
