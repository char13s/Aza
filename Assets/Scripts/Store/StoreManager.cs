using UnityEngine;
using UnityEngine.Events;
public class StoreManager:MonoBehaviour 
{
    public static UnityAction itemWasBought; 
    [SerializeField] private Items hpPotion;
    
    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    public void BuyItem() {
        if (Player.GetPlayer().stats.Exp >= 200) {
            Player.GetPlayer().stats.Exp -= 200;
        Player.GetPlayer().items.AddItem(hpPotion.data);}
        if (itemWasBought != null) {
            itemWasBought();

        }
        
    }
}
