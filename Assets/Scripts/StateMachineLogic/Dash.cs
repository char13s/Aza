using UnityEngine;
using XInputDotNetPure;
using UnityEngine.Events;
public class Dash : StateMachineBehaviour {
    [SerializeField] private GameObject burst;
    [SerializeField] private GameObject reminant;
    [SerializeField] private bool freefall;
    [SerializeField] private float move;
    private Player pc;

    private AudioClip sound;
    public static event UnityAction<AudioClip> dash;
    public static event UnityAction dashu;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (dashu != null) {
            dashu();
        }
        sound = AudioManager.GetAudio().Dash;
        pc = Player.GetPlayer();
        if (dash != null) {
            dash(sound);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        pc.CharCon.SimpleMove(Player.GetPlayer().transform.forward * 15);
    }

}
