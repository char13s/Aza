using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
public class SavePoint : MonoBehaviour
{
    [SerializeField] private Button save;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            save.interactable = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        save.interactable = true;
    }
    private void OnTriggerExit(Collider other)
    {
        save.interactable = false;
    }
}
