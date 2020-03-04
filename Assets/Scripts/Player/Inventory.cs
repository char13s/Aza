using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Inventory
{
    [SerializeField]
    private List<ItemData> items = new List<ItemData>();
    private int page = 0;

    [SerializeField] private Button useButton;
    //private List<string> itemNames = new List<string>();
    private List<GameObject> buttons = new List<GameObject>();
    
    public List<GameObject> Buttons { get => buttons; set => buttons = value; }
    public List<ItemData> Items { get => items; set => items = value; }


    // Start is called before the first frame update
    public void Start()
    {
        ItemData.ItemDataUpdate += UpdateInvent;
        GameController.onGameWasStarted += UpdateInvent;
    }
    public void AddItem(ItemData item)
    {

        if (HasItem(item.ID))
        {
            ItemData i = GetItem(item);

            i.Quantity++;
            //inventory.Add(item);
        }
        else
        {
			//UiManager.itemAdded(item.ItemDescription, SpriteAssign.SetImage(item));
            Items.Add(item);
            item.Quantity = 0;
            item.Quantity++;
            ButtonCreation(item);

        }
    }
    public ItemData GetItem(ItemData item)
    {
        foreach (ItemData i in Items)
        {
            if (i.ID == item.ID)
            {

                return i;
            }
        }

        return null;
    }
    public void UseItem(Items item)
    {

        item.data.Quantity--;//change this mess
        if (item.data.Quantity == 0)
        {
            Items.Remove(item.data);
        }

    }

    public void SellItem(Items item, int n)
    {
        item.data.Quantity -= n;
        if (item.data.Quantity == 0)
        {
            Items.Remove(item.data);
        }
    }
    public bool HasItem(int ID)
    {
        foreach (ItemData i in Items)
        {
            if (i.ID == ID)
            {
                return true;
            }
        }
        return false;
    }
    public void ButtonCreation(ItemData i)
    {
        GameObject c = new GameObject();
        c.name = "Item";
        c.AddComponent<Text>();
        c.AddComponent<Items>();
        c.GetComponent<Items>().data = i;
        c.GetComponent<Text>().text= i.ItemName;
        c.AddComponent<Button>();
        c.GetComponent<Text>().color = Color.black;
        c.GetComponent<Text>().font = UiManager.GetUiManager().LuckiestGuy;
        c.GetComponent<Text>().resizeTextForBestFit = true;
        c.transform.SetParent(SetToInvent(i));
        string d = i.ItemDescription;
        c.GetComponent<Button>().onClick.AddListener(c.GetComponent<Items>().IconClick);
        GameObject t = new GameObject();
        t.AddComponent<CanvasRenderer>();
        t.AddComponent<Text>();
        t.transform.SetParent(c.transform);
        GetQuantity(t.GetComponent<Text>(), i);
        t.transform.localPosition = new Vector3(-62, 0, 0);
        t.GetComponent<Text>().color = Color.black;
        t.GetComponent<Text>().font = UiManager.GetUiManager().LuckiestGuy;
        t.GetComponent<Text>().resizeTextForBestFit = true;
        Buttons.Add(c);
    }

    private Transform SetToInvent(ItemData i) {
        switch (i.Type)
        {
            case ItemData.ItemType.Normal:
                return UiManager.GetUiManager().ItemInvent.transform;
                
            default:
                break;
        }
        return null;

    }
    private void GetQuantity(Text t, ItemData i)
    {
        t.text = i.Quantity.ToString();
        if (i.Quantity == 0)
        {
            items.Remove(i);
        }

    }
    private void UpdateInvent()
    {
        if (buttons.Count != 0)
        {
            foreach (GameObject b in Buttons)
            {
                GetQuantity(b.transform.GetChild(0).GetComponent<Text>(), b.GetComponent<Items>().data);


                if (b.GetComponent<Items>().data.Quantity == 0)
                {
                    Debug.Log("Item was removed");
                    b.SetActive(false);
                }
            }
        }
    }
    
}

