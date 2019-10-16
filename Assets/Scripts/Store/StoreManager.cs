using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StoreManager:MonoBehaviour 
{
    public static UnityAction itemWasBought; 
    [SerializeField] private Items hpPotion;
    private Player player;
    private void Awake()
    {
        
    }
    private void Start()
    {
        player = Player.GetPlayer();
    }
    public void BuyItem() {
        if (player.Money >= 200) {
            player.Money -= 200;
        player.items.AddItem(hpPotion.data);}
        if (itemWasBought != null) {
            itemWasBought();

        }
        
    }
}
