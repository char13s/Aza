using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Cauldron : MonoBehaviour
{
    public static event UnityAction<int> potionMaking;
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
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("X"))
            {
                if (potionMaking != null)
                {
                    potionMaking(1);
                }
            }
        }
    }
}
