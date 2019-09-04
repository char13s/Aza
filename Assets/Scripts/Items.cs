using System;
using UnityEngine;
#pragma warning disable 0649
public class Items : MonoBehaviour
{
    [SerializeField] internal ItemData data = new ItemData();
    private bool crafting;

    //public Inventory list = new Inventory();


    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("started!");
        data.Start();
        CraftingTable.crafting += Crafting;
        UiManager.notCrafting += NotCrafting;
    }
    void Start()
    {
        
    }
    private void Crafting()
    {
        Debug.Log(crafting);
        Debug.Log("crafting is now true");
        crafting = true;
        Debug.Log(crafting);
    }

    private void NotCrafting() { Debug.Log("crafting is now false"); crafting = false; }
    public void IconClick()
    {


        if (crafting)
        {
            Debug.Log("Are you even crafting bro?");
            ItemSlot.ItemLastSelected = data;

        }
        else
        {
            if (!UiManager.UseMenu.activeSelf)
            {
                UiManager.UseMenu.SetActive(true);
                Debug.Log("bang!!");


                UiManager.UseButton.onClick.AddListener(data.UseItem);
                UiManager.GiveButton.onClick.AddListener(data.GiveItem);
                UiManager.ItemDescriptionButton.onClick.AddListener(data.DisplayDescription);
            }
            else
            {
                UiManager.UseMenu.SetActive(false);
            }
            //UiManager.ItemList.GetComponent<GridLayoutGroup>().enabled = false;
        }
        //itemDisplay.SetActive(true);

    }

    /*public void GetQuantity(Items item) {
        foreach (Items i in list.inventory) {
            if (i.ID == item.ID) {
                item.quantity++;
            }

        }

    }
    public void getID() { }
    
    public void getSellValue() { }
    public void IsKeyItem() { }*/

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                pc.PickUp(gameObject);
            }
        }
    }*/

}

