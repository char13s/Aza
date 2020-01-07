using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
public class ItemSlot : MonoBehaviour
{
    //private static ItemData itemLastSelected;
    //public static ItemData ItemLastSelected { get => itemLastSelected; set => itemLastSelected = value; }

    private ItemData data;
    [SerializeField] private Image itemSlotImage;
    private bool equipped;
    [SerializeField]private Items defaultItem;
    public enum ItemSlotType { Weapon, Shield, Mask };

    public static UnityAction setDefaults;
    public ItemData Data { get => data; }
    public bool IsSet => data != null;
    [SerializeField] private ItemSlotType type;


    public void Awake()
    {
        setDefaults+=SetDeafult;
    }
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Button>().onClick.AddListener(InventPopUp);
        //itemSlotImage = GetComponent<Image>();
        //CraftResults.crafted += EmptySlot;
        Debug.Log("Started item slot");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetDeafult() {
        Debug.Log("item added");
        if (defaultItem != null) {
            
            Player.GetPlayer().items.AddItem(defaultItem.data);
            Equipped(defaultItem.data);
        }
        
    }
    public void Equipped(ItemData item) {
        data = item;
        itemSlotImage.sprite = SpriteAssign.SetImage(item);

    }
    private void WeaponSlot() {

    }
    private void ShieldSlot()
    {

    }
    private void MaskSlot()
    {

    }
    /*private void TakeThisItem()
    {
        if (ItemLastSelected != null)
        {
            data = ItemLastSelected;
            itemSlotImage.sprite = SpriteAssign.SetImage(Data);
            ItemLastSelected.Quantity--;
            ItemLastSelected = null;
        }

    }
    private void EmptySlot() {

        itemSlotImage.sprite = null;
    }*/

}
