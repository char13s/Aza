using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    //This script knocks and other object this object his hitting back
    public float knockBackStrength;
    private void OnTriggerEnter(Collider other) {

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.Knocked(); 
        }
        /* Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            Debug.Log(knockBackStrength);
            Debug.Log("Smack");
            Vector3 direction = other.transform.position - transform.position;
            
            rb.AddForce(new Vector3(0,0,Mathf.Abs(other.gameObject.transform.position.z)) * knockBackStrength, ForceMode.Impulse);
            Debug.Log(other.gameObject.transform.position.z * knockBackStrength);
        }*/
        
    }
}
