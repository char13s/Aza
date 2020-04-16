using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ZaWarudo : MonoBehaviour
{

	public static event UnityAction timeFreeze;
    // Start is called before the first frame update
    private void OnEnable() {
        //Debug.Log("field is up");
    }
    private void OnDisable() {
        //Debug.Log("field is down");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Enemy")) {
            Debug.Log("freeze these fools");
            if (other.GetComponent<Enemy>() != null) {
                other.GetComponent<Enemy>().Frozen=true;

            }
			

		}
		
	}
}
