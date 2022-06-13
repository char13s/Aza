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
    
    
    public GameObject Aura { get => aura; set => aura = value; }
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Instantiate(gatherEnergy, Player.GetPlayer().transform.position, Quaternion.identity);
        //PostProcessorManager.GetProcessorManager().Transformation();
        //Player.GetPlayer().Transforming = true;
        //Player.GetPlayer().MoveSpeed *= 2;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.GetPlayer().PoweredUp) { 
        Instantiate(release, Player.GetPlayer().transform.position,Quaternion.identity);
        Instantiate(Aura, Player.GetPlayer().transform);
        //Player.GetPlayer().ZendHair.GetComponent<SkinnedMeshRenderer>().material= colorChange;
        
        }Player.GetPlayer().stats.Attack *= 2;
        Player.GetPlayer().stats.Defense *= 2;
        
        Player.GetPlayer().PowerUp = false;
        Player.GetPlayer().Transforming = false;
    }
    void FuckYou() { }
    
}
    
