using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockObj : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start() {

    }
    private void OnTriggerEnter(Collider collision) {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            //Debug.Log(knockBackStrength);
            Debug.Log("Smack");
            
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}

