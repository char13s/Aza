using UnityEngine;
using UnityEngine.Events;

public class DrawSword : StateMachineBehaviour {

    public static event UnityAction<AudioClip> hideSword;
    private AudioClip sound;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        sound = AudioManager.GetAudio().DrawSword;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (animatorStateInfo.normalizedTime > 0.9f ) {if (hideSword != null) {
                hideSword(sound);
            }
           
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
         
    }
}
