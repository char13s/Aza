using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    private static ItemData itemLastSelected;
    public static ItemData ItemLastSelected { get => itemLastSelected; set => itemLastSelected = value; }

    private ItemData data;
    private Image itemSlotImage;
    private bool crafting;

    public ItemData Data { get => data; }
    public bool IsSet => data != null;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TakeThisItem);
        itemSlotImage = transform.GetChild(0).GetComponent<Image>();
        CraftResults.crafted += EmptySlot;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void TakeThisItem()
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
    }
    
}
