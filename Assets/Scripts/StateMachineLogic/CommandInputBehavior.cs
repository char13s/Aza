using UnityEngine;
using XInputDotNetPure;
using UnityEngine.Events;
public class CommandInputBehavior : StateMachineBehaviour {
    private AudioClip swing;
    private AudioSource sound;
    private Player pc;
    private WeakZend wz;
    [SerializeField] private bool combo;
    private bool hit;
    private bool weakZend;
    [SerializeField]private bool stab;
	[SerializeField] private float move;
	[SerializeField] private GameObject slash;
    [SerializeField] private GameObject standoPowah;
    [SerializeField] private GameObject reminant;
    private bool fired;

    public static event UnityAction stopMove;
    public static event UnityAction resetMove;
    public static event UnityAction<AudioClip> sendsfx;
    private void Awake() {
        HitBox.onEnemyHit += MoveControl;
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        //sound=pc.Sfx;
        swing = AudioManager.GetAudio().Swing;
        /*if (jump) {
            swing = AudioManager.GetAudio().Jump;
        }
        else {
            swing = AudioManager.GetAudio().Swing;
        }*/
        //sound.PlayOneShot(swing);
        if (sendsfx != null) {
            sendsfx(swing);
        }
        //pc.Nav.enabled = false;
		//pc.RBody.isKinematic = false;
        //pc().Trail.SetActive(true);
        //pc.transform.position+= Player.GetPlayer().transform.forward * move*Time.deltaTime;
        //if (stab) {
        //    if (stopMove != null) {
        //        stopMove();
        //    }
        //}
        //GamePad.SetVibration(0,0.02f,0.02f);
        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //if (stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < 0.6f) {
        //    if (!hit) {
        //        pc.transform.position += pc.transform.forward * move * Time.deltaTime;
        //    }
        //    
        //    if (reminant != null) { 
        //    Instantiate(reminant, pc.transform.position, pc.transform.rotation);
        //    }
        //}
		//if (stateInfo.normalizedTime > 0.4&&slash!=null && stateInfo.normalizedTime < 0.6f&&!fired) {
		//	Instantiate(slash,pc.CenterPoint.transform.position, pc.transform.rotation);
        //    fired = true;
		//}
        //Player.GetPlayer().transform.position = Vector3.MoveTowards();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (standoPowah != null) { 
        //Instantiate(standoPowah, pc.transform.position , pc.transform.rotation);
        //}
        //hit = false;
        //fired = false;
        //GamePad.SetVibration(0, 0, 0);
        //if (resetMove != null) {
        //    resetMove();
        //}
        //pc.MoveSpeed = 6;
        //Player.GetPlayer().Trail.SetActive(false); 
        //Player.GetPlayer().Nav.enabled = true;
    }
    private void MoveControl() {
        if (!stab) { 
          // hit = true;
        }
    }

    

}
