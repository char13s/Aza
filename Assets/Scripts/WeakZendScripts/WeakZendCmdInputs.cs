using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;
public class WeakZendCmdInputs : StateMachineBehaviour {
    private AudioClip swing;
    private WeakZend wz;

    private bool hit;
    
    
    [SerializeField] private float move;

    public static event UnityAction<AudioClip> sendSound;
    private void Awake() {
        HitBox.onEnemyHit += MoveControl;
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        wz = WeakZend.GetWeakZend();
        //sound = wz.Sfx;
        swing = AudioManager.GetAudio().Swing;
        if (sendSound != null) {
            sendSound(swing);
        }
        wz.CmdInput = 0;
        wz.MoveSpeed = 0;
        wz.Nav.enabled = false;
        wz.Rbody.isKinematic = false;
        //pc().Trail.SetActive(true);
        wz.transform.position += Player.GetPlayer().transform.forward * move * Time.deltaTime;

        GamePad.SetVibration(0, 0.02f, 0.02f);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetInput();
        if (stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < 0.6f) {
            if (!hit) {
                wz.transform.position += wz.transform.forward * move * Time.deltaTime;
            }            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
        hit = false;
        GamePad.SetVibration(0, 0, 0);
        
    }
    private void MoveControl() {
        hit = true;
    }
    private void GetInput() {
        if (Input.GetButtonDown("Square")) {
            wz.CmdInput = 1;
        }

        if (Input.GetButtonDown("Triangle")) {
            wz.CmdInput = 2;
        }

    }
}
