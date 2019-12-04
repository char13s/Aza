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
    [FormerlySerializedAs("ID")]
    [SerializeField] private int id;
    private int quantity;
    [SerializeField] private int sellableValue;
    [FormerlySerializedAs("keyItem")]
    [SerializeField] private bool shield;
    [FormerlySerializedAs("recipe")]
    [SerializeField] private bool mask;
    [SerializeField] private bool weapon;
    [SerializeField] private bool selling;
   
    

    //[SerializeField] private Sprite sprite;
    [SerializeField] private string itemDescription;

    public static UnityAction useMenuUp;
    //private GameObject useMenu;
    public static UnityAction ItemDataUpdate;
    public int ID { get => id; set => id = value; }
    public int Quantity { get => quantity; set { quantity = Mathf.Clamp(value, 0, 999); if (ItemDataUpdate != null) { ItemDataUpdate(); } } }
    public int SellableValue { get => sellableValue; set => sellableValue = value; }
    public bool KeyItem { get => shield; set => shield = value; }
    public bool Recipe { get => mask; set => mask = value; }
    public bool Weapon { get => weapon; set => weapon = value; }
    public bool Selling { get => selling; set => selling = value; }

    //public Sprite Sprite { get => sprite; set => sprite = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public ItemType Type { get => type; set => type = value; }

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
            case 1:
                Player.GetPlayer().stats.HealthLeft += 5;
                break;
            case 100:
                Player.GetPlayer().stats.HealthLeft += 50;
                break;

        }
        Quantity--;
        UiManager.UseMenu.SetActive(false);

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
