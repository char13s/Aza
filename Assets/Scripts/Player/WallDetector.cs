using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WallDetector : MonoBehaviour
{

    private Player pc;
    
    // Start is called before the first frame update
    void Start()
    {
        
        pc = Player.GetPlayer();

    }
    private void OnDisable()
    {
        pc.Wall = false;
    }
   void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")&&!other.isTrigger&&!other.gameObject.CompareTag("Enemy") )
        {
            
            pc.Wall = true;
        }   
    }
    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.isTrigger&& !other.gameObject.CompareTag("Item") &&!other.gameObject.GetComponent<FrontDoor>()&&!other.gameObject.CompareTag("Enemy"))
        {
            
            pc.Wall = true;
        }
        if (other.gameObject.CompareTag("ClimbableWall"))
        {
            Debug.Log("thefuckman");
            if (Input.GetButtonDown("X")){
                
                
                pc.Climbing1 = true;

            }
            
        }
        if (other.gameObject.CompareTag("Item")) {
            if (Input.GetButtonDown("X"))
            {
                pc.PickUp(other.gameObject.GetComponent<Items>());
            }
        }
        if (other.gameObject.CompareTag("Log"))
        {
            if (Input.GetButtonDown("X"))
            {

                pc.Chopping = true;

            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        pc.Climbing1 = false;
        pc.Wall = false;
        pc.WallMoving = false;

    }
}
