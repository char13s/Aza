using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : StateMachineBehaviour
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
        if (stateInfo.normalizedTime >= 0.49 && !preformed)
        {
            Fire();
            
                Instantiate(energyWave, Mage.GetMage().Origin.transform.position , Quaternion.identity);
            
        }
    }
    private void Fire()
    {
        preformed = true;
    }
}
