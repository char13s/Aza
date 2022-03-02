using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidArea : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        print("Entered");
        
    }
    private void OnTriggerStay(Collider other) {
        Debug.Log("shrinking");
        other.transform.localScale=Vector3.Slerp(other.transform.localScale, new Vector3(0, 0, 0),Time.deltaTime*speed );
    }
}
