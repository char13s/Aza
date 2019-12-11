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
    private Image itemSlotImage;
    private bool equipped;
    public enum ItemSlotType { Weapon, Shield, Mask };
    public ItemData Data { get => data; }
    public bool IsSet => data != null;
    [SerializeField] private ItemSlotType type;

    

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Button>().onClick.AddListener(InventPopUp);
        //itemSlotImage = transform.GetChild(0).GetComponent<Image>();
        //CraftResults.crafted += EmptySlot;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Equipped() {
        switch (type) {
            case ItemSlotType.Weapon:

                break;
            case ItemSlotType.Shield:

                break;
            case ItemSlotType.Mask:

                break;
        }


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
