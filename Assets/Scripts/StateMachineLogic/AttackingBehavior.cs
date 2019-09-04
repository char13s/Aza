using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class AttackingBehavior : StateMachineBehaviour
{
    private GameObject castingFlame;
    //[SerializeField] private GameObject slash;
    private bool pressed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pressed = false;
        Player.GetPlayer().HitBox.SetActive(true);
       //current = ;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float time=1f;
        if (Input.GetButtonDown("X") && Player.GetPlayer().stats.StaminaLeft > 0&&!pressed)
        {
            pressed=true;
            Player.GetPlayer().HitCounter++;
        }
        switch(Player.GetPlayer().HitCounter)
        {
            case 0:
                time = 0.4f;
                break;
            case 1:
                time = 0.4f;
                break;
            case 2:
                time = 1f;
                //Time.timeScale = 0.5f;
                break;
        }
        if (stateInfo.normalizedTime > time) {Player.GetPlayer().HitBox.SetActive(false);  }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Pressed: "+pressed);
        if (!pressed)
        {
            Player.GetPlayer().HitCounter = 0;
            Player.GetPlayer().Attack = false;
        }
        if (Player.GetPlayer().HitCounter == 3)
        {
            Player.GetPlayer().HitCounter = 0;
            
            Player.GetPlayer().Attack = false;
        }
        pressed = false;
        
    }
    
}
