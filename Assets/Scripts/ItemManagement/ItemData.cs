using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
[System.Serializable]
public class ItemData
{
    public enum ItemType {Normal,Weapon, Shield, Mask  }
    [SerializeField] private ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private int id;
    private int quantity;
    
    
   
    

    //[SerializeField] private Sprite sprite;
    [SerializeField] private string itemDescription;

    public static UnityAction useMenuUp;
    //private GameObject useMenu;
    public static UnityAction ItemDataUpdate;
    public int ID { get => id; set => id = value; }
    public int Quantity { get => quantity; set { quantity = Mathf.Clamp(value, 0, 999); if (ItemDataUpdate != null) { ItemDataUpdate(); } } }
    
    

    //public Sprite Sprite { get => sprite; set => sprite = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public ItemType Type { get => type; set => type = value; }
    public string ItemName { get => itemName; set => itemName = value; }

    //public int Power { get => power; set => power = value; }

    public void Awake()
    {
        
        GameController.onGameWasStarted += NullQuantity;
        Debug.Log("started!");
    }
    //public GameObject UseMenu { get => useMenu; set => useMenu = value; }
    public void Start()
    {

        
        
    }
    private void NullQuantity() { Debug.Log("Quantity nulled"); quantity = 0; }
    public void UseItem()
    {
        switch (id)
        {
            case 0:
                Player.GetPlayer().stats.HealthLeft += 5;
                break;
            case 1://small potion
                Player.GetPlayer().stats.HealthLeft += (int)(Player.GetPlayer().stats.Health/0.2f);
                break;
            case 2://medium potion
                Player.GetPlayer().stats.HealthLeft += (int)(Player.GetPlayer().stats.Health / 0.4f);
                break;
            case 3://large potion
                Player.GetPlayer().stats.HealthLeft += (int)(Player.GetPlayer().stats.Health / 0.7f);
                break;
            case 4://full restore
                Player.GetPlayer().stats.HealthLeft += Player.GetPlayer().stats.Health;
                break;
            case 5://Status curer
                Player.GetPlayer().status.Status=StatusEffects.Statuses.neutral;
                break;
            case 6://Attack Up
                break;
            case 7://Defense Up
                break;
            case 8: //Mp regen
                break;
            case 9://Hp regen
                break;
            case 10://Heal poison
                BleedingHeal();
                break;
            case 11://Heal paralysis
                ParalysisHeal();
                break;
            case 12://Heal burn
                BurnHeal();
                break;
        }
        Quantity--;
        UiManager.UseMenu.SetActive(false);

    }

    private void BurnHeal() {
        if (Player.GetPlayer().status.Status == StatusEffects.Statuses.burned) {
            Player.GetPlayer().status.Status = StatusEffects.Statuses.neutral;
        }
    }
    private void ParalysisHeal() {
        if (Player.GetPlayer().status.Status == StatusEffects.Statuses.stunned) {
            Player.GetPlayer().status.Status = StatusEffects.Statuses.neutral;
        }
    }
    private void BleedingHeal() {
        if (Player.GetPlayer().status.Status == StatusEffects.Statuses.bleeding) {
            Player.GetPlayer().status.Status = StatusEffects.Statuses.neutral;
        }
    }
    public void GiveItem()
    {
        Debug.Log("Here you go");
    }
    public void DisplayDescription()
    {
        Debug.Log("Words words words");
    }
    public void Drop()
    {
        Quantity = 0;
    }



}
