using UnityEngine;
using XInputDotNetPure;
using UnityEngine.Events;
public class CommandInputBehavior : StateMachineBehaviour {
    private AudioClip swing;
    private AudioSource sound;
    private Player pc;
    private WeakZend wz;
    [SerializeField] private bool jump;
    private bool hit;
    private bool weakZend;
    [SerializeField]private bool stab;
	[SerializeField] private float move;
	[SerializeField] private GameObject slash;
    [SerializeField] private GameObject standoPowah;
    [SerializeField] private GameObject reminant;

    public static event UnityAction stopMove;
    public static event UnityAction resetMove;
    private void Awake() {
        HitBox.onEnemyHit += MoveControl;
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = Player.GetPlayer();
        sound=pc.Sfx;
        swing = AudioManager.GetAudio().Swing;
        if (jump) {
            swing = AudioManager.GetAudio().Jump;
        }
        else {
            swing = AudioManager.GetAudio().Swing;
        }
        sound.PlayOneShot(swing);
        pc.CmdInput = 0;
        pc.MoveSpeed = 0;
        pc.Nav.enabled = false;
		pc.RBody.isKinematic = false;
        //pc().Trail.SetActive(true);
        pc.transform.position+= Player.GetPlayer().transform.forward * move*Time.deltaTime;
        if (stab) {
            if (stopMove != null) {
                stopMove();
            }
        }
        GamePad.SetVibration(0,0.02f,0.02f);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetInput();
        if (stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < 0.6f) {
            if (!hit) {
                pc.transform.position += pc.transform.forward * move * Time.deltaTime;
            }
            
            if (reminant != null) { 
            Instantiate(reminant, pc.transform.position, pc.transform.rotation);
            }
        }
		if (stateInfo.normalizedTime > 0.4&&slash!=null && stateInfo.normalizedTime < 0.6f) {
			Instantiate(slash,pc.CenterPoint.transform.position, pc.transform.rotation);
		}
        //Player.GetPlayer().transform.position = Vector3.MoveTowards();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (standoPowah != null) { 
        Instantiate(standoPowah, pc.transform.position , pc.transform.rotation);
        }
        hit = false;
        GamePad.SetVibration(0, 0, 0);
        if (resetMove != null) {
            resetMove();
        }
        //pc.MoveSpeed = 6;
        //Player.GetPlayer().Trail.SetActive(false); 
        //Player.GetPlayer().Nav.enabled = true;
    }
    private void MoveControl() {
        if (!stab) { 
           hit = true;}
    }
    private void GetInput() {
        if (Input.GetButtonDown("Square"))
        {
            pc.CmdInput = 1;
        }

        if (Input.GetButtonDown("Triangle"))
        {
            pc.CmdInput = 2;
        }

    }
    

}
