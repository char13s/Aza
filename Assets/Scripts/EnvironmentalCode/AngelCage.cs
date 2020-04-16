using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AngelCage : MonoBehaviour
{
    private Rigidbody rb;

    public static event UnityAction hitGround;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        
            rb.useGravity = true;

        if (other.gameObject.layer == 11) {
            if (hitGround != null) {
                hitGround();
            }
        }
    }
}
