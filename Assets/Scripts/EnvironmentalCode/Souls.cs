using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Souls : MonoBehaviour
{

    public static event UnityAction soulCount;
    public static event UnityAction soundOff;
    [SerializeField] private GameObject collect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (soulCount != null) {
                soulCount();
            }
            if (soundOff != null) {
                soundOff();
            }
            Instantiate(collect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
