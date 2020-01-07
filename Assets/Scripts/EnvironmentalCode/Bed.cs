using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject bedspot;
    [SerializeField] private GameObject blanket;
    [SerializeField] private GameObject outaBedspot;
    public static UnityAction<GameObject,GameObject> bed;
    private void Awake()
    {
        Player.notSleeping += NotSleep;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void NotSleep() {
        blanket.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("X"))
            {
                if (bed != null) {
                    bed(bedspot,outaBedspot);
                }
                blanket.SetActive(true);
                
            }
        }
    }
}
