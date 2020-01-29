using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ZaWarudo : MonoBehaviour
{

	public static event UnityAction timeFreeze;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Enemy")) {
			other.GetComponent<Enemy>().Frozen=true;

		}
		
	}
}
