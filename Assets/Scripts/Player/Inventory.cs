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
    private GameObject pocket;
    private GameObject pageTitle;
    private GameObject pageNum;
    private List<GameObject> buttons = new List<GameObject>();
    private GameObject lastItemSelected;
    private bool pocketActive;


    public bool PocketActive { get => pocketActive; set => pocketActive = value; }
    public int Page { get => page; set => page = value; }
    public List<ItemData> Items { get => items; set => items = value; }
    public GameObject Pocket { get => pocket; set => pocket = value; }
    public GameObject PageTitle { get => pageTitle; set => pageTitle = value; }
    public GameObject PageNum { get => pageNum; set => pageNum = value; }
    public List<GameObject> Buttons { get => buttons; set => buttons = value; }


    // Start is called before the first frame update
    public void Start()
    {
        ItemData.ItemDataUpdate += UpdateInvent;
        GameController.onGameWasStarted += UpdateInvent;
        CraftingTable.crafting += SetToCraftingMenu;
        UiManager.notCrafting += SetBackToInvent;
    }
    public void AddItem(ItemData item)
    {

        if (HasItem(item.ID))
        {
            ItemData i = GetItem(item);
            Debug.Log("OKay");
            i.Quantity++;
            //inventory.Add(item);
        }
        else
        {
            Items.Add(item);
            item.Quantity++;
            ButtonCreation(item);

        }
    }
    private void SetToCraftingMenu()
    {

        foreach (GameObject b in Buttons)
        {
            b.transform.SetParent(UiManager.ItemList.transform);

        }
    }
    private void SetBackToInvent() {
        foreach (GameObject b in Buttons)
        {
            b.transform.SetParent(Pocket.transform);

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

    public void DisplayInventory()
    {
        if (!PocketActive)
        {
            PocketActive = true;
            Pocket.SetActive(true);
        }
        switch (Page)
        {
            case 0:
                PageTitle.GetComponent<Text>().text = "Materials";
                PageNum.GetComponent<Text>().text = "0";
                break;
            case 1:
                PageTitle.GetComponent<Text>().text = "Craft Recipes";
                PageNum.GetComponent<Text>().text = "1";
                break;
            case 2:
                PageTitle.GetComponent<Text>().text = "Weapons";
                PageNum.GetComponent<Text>().text = "2";
                break;
            case 3:
                PageTitle.GetComponent<Text>().text = "Key Items";
                PageNum.GetComponent<Text>().text = "3";
                break;
        }
    }
    private void DeleteEmptyItems()
    {
        foreach (ItemData i in items)
        {

        }
    }
    public void InventOff()
    {
        Pocket.SetActive(false);
        PocketActive = false;
    }

    public void ButtonCreation(ItemData i)
    {


        GameObject c = new GameObject();
        c.name = "Item";
        c.AddComponent<Image>();
        c.AddComponent<Button>();
        c.AddComponent<Items>();
        c.GetComponent<Items>().data = i;
        c.GetComponent<Image>().sprite = SpriteAssign.SetImage(i); ;
        c.transform.SetParent(Pocket.transform);
        //c.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        //c.transform.localPosition = new Vector3(0, 0, 2);
        string d = i.ItemDescription;
        c.GetComponent<Button>().onClick.AddListener(c.GetComponent<Items>().IconClick);
        c.AddComponent<SelfDestruct>();

        //c.AddComponent<CanvasGroup>();
        //c.GetComponent<CanvasGroup>().interactable=false;
        //c.GetComponent<FollowMouse>().enabled=false;
        GameObject t = new GameObject();
        t.AddComponent<CanvasRenderer>();
        t.AddComponent<Text>();
        t.transform.SetParent(c.transform);
        GetQuantity(t.GetComponent<Text>(), i);
        t.transform.localPosition = new Vector3(0, 0, 8);
        t.GetComponent<Text>().color = Color.black;
        t.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        t.GetComponent<Text>().resizeTextForBestFit = true;
        Buttons.Add(c);
    }

    private void OnButtonHold()
    {

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
                GetQuantity(b.GetComponentInChildren<Text>(), b.GetComponent<Items>().data);
                Debug.Log("Invent was updated");
                if (b.GetComponent<Items>().data.Quantity == 0)
                {
                    Debug.Log("Item was removed");
                    b.SetActive(false);
                }
            }
        }
    }
    //DisplayInventory();
}

