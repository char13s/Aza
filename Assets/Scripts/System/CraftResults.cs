using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CraftResults : MonoBehaviour
{


    private Image itemResultImage;

    [SerializeField] private ItemSlot itemSlot1;
    [SerializeField] private ItemSlot itemSlot2;
    private static CraftableItem craftedItem;
    [Header("Craftable Items")]
    [SerializeField] private CraftableItem hpPotion;
    [SerializeField] private CraftableItem bridgeParts;

    public static UnityAction crafted;

    public static CraftableItem CraftedItem { get => craftedItem; set => craftedItem = value; }

    private void Start()
    {
        itemResultImage = transform.GetChild(0).GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(PickUp);
    }
    public void CraftItem()
    {

        /*for (int i = 0; i < CItems.Length; i++) {
            CraftableItem c= CItems[i];
            c.Craft(itemSlot1.Data,itemSlot2.Data);
        }*/
        if (itemSlot1.Data != null && itemSlot2.Data != null)
        {
            itemResultImage.sprite = SpriteAssign.SetImage(hpPotion.Craft(itemSlot1.Data, itemSlot2.Data));
            bridgeParts.Craft(itemSlot1.Data, itemSlot2.Data);

        }

    }
    private void PickUp()
    {
        if (craftedItem != null)
        {
            Player.GetPlayer().items.AddItem(craftedItem.GetComponent<Items>().data);
            craftedItem = null;
            itemResultImage.sprite = null;
            //UiManager.CraftMenu.SetActive(false);
            if (crafted != null)
            {
                crafted();
            }
        }


    }
}
