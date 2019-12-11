using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrines : MonoBehaviour
{
    [SerializeField] private int amountRequired;
    [SerializeField] private int exp;
    [SerializeField] private GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OpenPortal() {

        if (exp >= amountRequired) {

            portal.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            if(Input.GetButtonDown("X")){


            }
        }
    }
}
