using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour
{
    private Player pc;
    private bool ground;
    public static event UnityAction<bool> groundStatus;
    // Start is called before the first frame update
    private void Start()
    {
        
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
    private void IsGrounded() {

        if (pc.Nav.isOnNavMesh)
        {
            pc.Grounded = true;
        }
        else
        {
            pc.Grounded = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null )
        {

            ground = true;
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
