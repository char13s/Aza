using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastingStateMachine : StateMachineBehaviour
{
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject arrow;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (AzaAi.GetAza().Animations) {
            case 2:
                Instantiate(health, Player.GetPlayer().transform.position,Quaternion.identity);
                break;

        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
            
            
        
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        AzaAi.GetAza().HealCast.SetActive(false);
        switch (AzaAi.GetAza().Animations)
        {
            case 3:
                Debug.Log("FireBallShot");
                Instantiate(fireball, animator.rootPosition, Quaternion.identity);
                fireball.transform.position = animator.rootPosition;
                break;
            case 4:
                //Debug.Log("FireBallShot");
                Instantiate(arrow, AzaAi.GetAza().AzaBow.transform.position, Quaternion.identity);
                arrow.transform.position = AzaAi.GetAza().AzaBow.transform.position;
                break;

        }
        AzaAi.GetAza().State = AzaAi.AzaAiStates.Idle;
        AzaAi.GetAza().Animations = 0;
        
        

    }
}
