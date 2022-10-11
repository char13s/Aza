using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ParaylsisPoints : MonoBehaviour
{
    public static event UnityAction stun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //stop player from falling for a short amount of time
        stun.Invoke();
    }
}
