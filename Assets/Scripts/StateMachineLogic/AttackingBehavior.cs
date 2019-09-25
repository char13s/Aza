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
        Player.GetPlayer().RBody.isKinematic = false;

        Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward * 105, ForceMode.VelocityChange);

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (stateInfo.normalizedTime > 0.9) { Player.GetPlayer().RBody.isKinematic = true; }
        float time = 0;
        if (Input.GetButtonDown("X") && !pressed)
        {
            pressed = true;
            Player.GetPlayer().HitCounter++;
        }
        switch (Player.GetPlayer().HitCounter)
        {
            case 0:
                time = 0.8f;
                break;
            case 1:
                time = 0.9f;
                break;
            case 2:
                time = 0.9f;
                break;
            case 3:
                time = 0.5f;
                break;
        }
        if (stateInfo.normalizedTime > time) { Player.GetPlayer().HitBox.SetActive(false); }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Pressed: "+pressed);
        if (!pressed)
        {
            Player.GetPlayer().HitCounter = 0;

        }
        if (Player.GetPlayer().HitCounter >= 3)
        {
            Player.GetPlayer().HitCounter = 0;


        }
        pressed = false;

    }

}
