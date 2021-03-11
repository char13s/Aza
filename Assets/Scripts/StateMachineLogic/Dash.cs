using UnityEngine;
using XInputDotNetPure;
using UnityEngine.Events;
public class Dash : StateMachineBehaviour {
    [SerializeField] private GameObject burst;
    [SerializeField] private GameObject reminant;
    [SerializeField] private bool freefall;
    private Player pc;

    private AudioClip sound;
    public static event UnityAction<AudioClip> dash;
    public static event UnityAction dashu;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Player.GetPlayer().LeftHand.SetActive(true);
        //Player.GetPlayer().RightHand.SetActive(true);
        //Player.GetPlayer().DevilFoot.SetActive(true);
        if (dashu != null) {
            dashu();
        }
        sound = AudioManager.GetAudio().Dash;
        pc = Player.GetPlayer();
        if (burst != null) {
            Instantiate(burst, pc.transform.position, pc.transform.rotation);
        }
        //pc.RBody.AddForce(pc.transform.forward * 1000, ForceMode.Impulse);
        //burst.transform.position=
        if (dash != null) {
            dash(sound);
        }
        pc.Rbody.velocity = new Vector3(0,0,0);
        pc.Rbody.useGravity = false;
        GamePad.SetVibration(0, 0.2f, 0.1f);
        pc.Rbody.AddForce(pc.transform.forward * 8f, ForceMode.VelocityChange);
        pc.Rbody.AddForce(pc.transform.forward * 150f, ForceMode.Impulse);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //pc.RBody.drag = 0;
        //if (freefall) {
        //    if (FreeFallZend.GetFreeFallingZend().Falling) {
        //        Instantiate(reminant, FreeFallZend.GetFreeFallingZend().transform.position + new Vector3(0, 0, -0.25f), FreeFallZend.GetFreeFallingZend().transform.rotation);
        //    }
        //}
        
            Instantiate(reminant, pc.transform.position, pc.transform.rotation);
        
        //pc.RBody.isKinematic = false;
       //pc.RBody.AddForce(pc.transform.forward*900, ForceMode.Impulse);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        pc.Rbody.useGravity = true;
       
       
        GamePad.SetVibration(0, 0, 0);
    }

}
