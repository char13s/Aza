using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Player pc;
    
    // Start is called before the first frame update
    void Start()
    {
        
        pc = Player.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();

    }
    void IsGrounded() {

        if (pc.Nav.isOnNavMesh)
        {
            pc.Grounded = true;
        }
        else
        {
            pc.Grounded = false;
        }
        
    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && !other.gameObject.CompareTag("Player"))
        {
            pc.Grounded = true;

        }
        

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject!=null && !other.gameObject.CompareTag("Player"))
        {
            pc.Grounded = true;

        }
        else { pc.Grounded = false; }

    }
    void OnTriggerExit(Collider other) {

        pc.Grounded = false;
    }*/


}
