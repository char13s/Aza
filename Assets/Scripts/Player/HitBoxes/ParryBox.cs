using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ParryBox : MonoBehaviour
{
    public static event UnityAction parry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        parry.Invoke();
        Debug.Log("Player got parried");
    }
}
