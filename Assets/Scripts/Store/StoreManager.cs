using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager 
{
    private int money;
    public int Money { get {return money; } set { money = value; } }
    private List<Items> itemsForSale = new List<Items>();
    
    public void PutUpForSale(Items i)
    {
        itemsForSale.Add(i);
    }
    public void Sales()
    {
        if (itemsForSale.Count > 0)
        {
            Items i = itemsForSale[0];
            Money += i.data.SellableValue;
        }
    }

}
