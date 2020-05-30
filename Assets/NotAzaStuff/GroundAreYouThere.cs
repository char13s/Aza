using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAreYouThere : MonoBehaviour
{
    [SerializeField] private Ralux ral;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        ral.Grounded = true;
    }
    private void OnTriggerExit(Collider other) {
        ral.Grounded = false;
    }
}
