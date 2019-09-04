using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItem:MonoBehaviour {

    [SerializeField] private int ID1;
    [SerializeField] private int ID2;

    public ItemData Craft(ItemData item1,ItemData item2)
    {
        if (item1.ID == ID1 && item2.ID == ID2)
        {
            Debug.Log("Item made!");
            CraftResults.CraftedItem = this;
            return GetComponent<Items>().data;
        }
        Debug.Log("fawk");
        return null;
    }
}
