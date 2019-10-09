using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSaiyanBehavior : StateMachineBehaviour
{
    [SerializeField] private float change;
    [SerializeField] private Material colorChange;
    [SerializeField] private GameObject gatherEnergy;
    [SerializeField] private GameObject release;
    [SerializeField] private GameObject aura;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(gatherEnergy, Player.GetPlayer().transform.position, Quaternion.identity);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(release, Player.GetPlayer().transform.position,Quaternion.identity);
        Instantiate(aura, Player.GetPlayer().transform);
        Player.GetPlayer().ZendHair.GetComponent<SkinnedMeshRenderer>().material= colorChange;
        Player.GetPlayer().stats.Attack *= 50;
        Player.GetPlayer().stats.Defense *= 50;
        Player.GetPlayer().MoveSpeed *= 2;
        Player.GetPlayer().PowerUp = false;
    }
}
    
