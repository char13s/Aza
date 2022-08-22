using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySlash : StateMachineBehaviour {
    [SerializeField] private GameObject energyWave;
    [SerializeField] private float blastTime;
    [SerializeField] private SlashSpawner slashSpawner;
    private bool preformed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        preformed = false;
        //slashSpawner=Player.GetPlayer().PlayerBody.SlashSpawner;
        //Instantiate(energyWave, transform.position , Quaternion.identity);
        //slashSpawner.SpawnSlash();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime >= 0.49 && !preformed) {
            Fire();
            
        }

    }
    private void Fire() {
        preformed = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


    }
    
}
