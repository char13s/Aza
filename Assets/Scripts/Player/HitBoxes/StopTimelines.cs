using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class StopTimelines : MonoBehaviour
{
    public static event UnityAction stopTimelines;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (stopTimelines != null) {
            stopTimelines();
        }
    }
}
