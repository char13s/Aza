using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CraftingTable : MonoBehaviour
{

    public static UnityAction crafting;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("X"))
            {

                Crafting();

            }
        }
    }
    private void Crafting()
    {
        Debug.Log("Ah shit");
        //UiManager.CraftMenu.SetActive(true);
        if (crafting != null)
        {
            crafting();
            Debug.Log("Ah shit");
        }
    }
}
