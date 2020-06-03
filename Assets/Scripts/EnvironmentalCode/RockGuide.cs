using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGuide : MonoBehaviour
{
    [SerializeField]private GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("okayyyy");
            if (Input.GetButtonDown("X"))
            {
                //Player.GetPlayer().Nav.SetDestination(destination.transform.position); 
            }
        }
    }
}
