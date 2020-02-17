using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySlash : StateMachineBehaviour
{
    [SerializeField] private GameObject energyWave;
    [SerializeField] private float blastTime;
     private bool preformed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        preformed = false;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >=0.49&& !preformed ) {
            Fire();
                        Instantiate(energyWave, Player.GetPlayer().transform.position+new Vector3(0,0.4F,0), Quaternion.identity);}
        
    }
    private void Fire() {
        preformed = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }
}
