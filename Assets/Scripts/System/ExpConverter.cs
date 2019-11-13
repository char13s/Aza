using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ExpConverter : MonoBehaviour
{
    public static UnityAction levelMenuUp;
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
        if (other.gameObject.CompareTag("Player")) {
            if (Input.GetButtonDown("X")) {
                if (levelMenuUp != null) {
                    levelMenuUp();
                }
                Debug.Log("player is here");
            }

        }
    }
}
