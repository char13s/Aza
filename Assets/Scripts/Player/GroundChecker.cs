using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour {
    private Player pc;
    private bool ground;
    private AudioClip landing;
    public static event UnityAction<bool> groundStatus;
    public static event UnityAction<AudioClip> landed;
    // Start is called before the first frame update
    private void Awake() {
        
       
    }
    private void Start()
    {landing = AudioManager.GetAudio().LandingSound;

        StartCoroutine(Wait());
        
    }
    private IEnumerator Wait() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return null;
        pc = Player.GetPlayer();
    }
    // Update is called once per frame
    private void Update()
    {
        if (groundStatus != null)
        {
            groundStatus(ground);
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null &&!pc.Flying)
        {

            ground = true;
            if (landed!=null) {
                landed(landing);
            }
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject!=null ){
            ground = true;
        }
        else {
            ground = false;
        }

    }
    private void OnTriggerExit(Collider other) {

        ground = false;
    }


}
