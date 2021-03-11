using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour {
    private Player player;
    private AudioClip landing;
    private float distanceGround;
    public static event UnityAction<bool> groundStatus;
    public static event UnityAction<AudioClip> landed;
    // Start is called before the first frame update
    private void Awake() {
        
       
    }
    private void Start()
    {
        
        landing = AudioManager.GetAudio().LandingSound;

       
        distanceGround = GetComponent<Collider>().bounds.extents.y;
        player = Player.GetPlayer();
    }
    
    // Update is called once per frame
    /*private void Update()
    {private bool ground;
    
        if (groundStatus != null)
        {
            groundStatus(ground);
        }

    }
    //private void IsGrounded() {
    //
    //    if (pc.Nav.isOnNavMesh)
    //    {
    //        pc.Grounded = true;
    //    }
    //    else
    //    {
    //        pc.Grounded = false;
    //    }
    //    
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null )
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
    */
    private void FixedUpdate() {
        if (!Physics.Raycast(transform.position, -Vector2.up, distanceGround + 0.2f)) {
            player.Grounded = false;

        }
        else {
            player.Grounded = true;

        }
    }
}
