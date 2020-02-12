using UnityEngine;
using XInputDotNetPure;
public class CommandInputBehavior : StateMachineBehaviour
{
    private AudioClip swing;
    private AudioSource sound;
	[SerializeField] private float move;
	[SerializeField] private GameObject slash;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sound=Player.GetPlayer().Sfx;
        swing = AudioManager.GetAudio().Swing;
        sound.PlayOneShot(swing);
        Player.GetPlayer().CmdInput = 0;
        Player.GetPlayer().MoveSpeed = 0;
        Player.GetPlayer().Nav.enabled = false;
		Player.GetPlayer().RBody.isKinematic = false;
		//Player.GetPlayer().Trail.SetActive(true);
		Player.GetPlayer().transform.position+= Player.GetPlayer().transform.forward * move*Time.deltaTime;
        GamePad.SetVibration(0,0.2f,0.2f);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetInput();
        if (stateInfo.normalizedTime > 0.1f && stateInfo.normalizedTime < 0.6f) {
            Player.GetPlayer().transform.position += Player.GetPlayer().transform.forward * move * Time.deltaTime;
        }
		if (stateInfo.normalizedTime > 0.4&&slash!=null && stateInfo.normalizedTime < 0.6f) {
			Instantiate(slash,Player.GetPlayer().transform.position+ Player.GetPlayer().transform.forward, Player.GetPlayer().transform.rotation);
		}
        //Player.GetPlayer().transform.position = Vector3.MoveTowards();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GamePad.SetVibration(0, 0, 0);
        //Player.GetPlayer().Trail.SetActive(false); 
        //Player.GetPlayer().Nav.enabled = true;
    }
    private void HitBoxControl() {
        
    }
    private void GetInput() {
        if (Input.GetButtonDown("Square"))
        {
            Player.GetPlayer().CmdInput = 1;
        }

        if (Input.GetButtonDown("Triangle"))
        {
            Player.GetPlayer().CmdInput = 2;
        }

    }

}
