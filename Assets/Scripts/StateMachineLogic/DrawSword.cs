using UnityEngine;
using UnityEngine.Events;

public class DrawSword : StateMachineBehaviour {

    public static event UnityAction<AudioClip> hideSword;
    private AudioClip sound;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        sound = AudioManager.GetAudio().DrawSword;
        if (hideSword != null) {
            hideSword(sound);
        }
    }
}
