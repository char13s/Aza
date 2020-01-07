using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0649
public class Items : MonoBehaviour
{
    [SerializeField] internal ItemData data = new ItemData();


    public static UnityAction onItemClick;


    // Start is called before the first frame update
    private void Awake()
    {

        data.Start();

    }
    void Start()
    {

    }

    public void IconClick()
    {
        switch (data.Type)
        {
            case ItemData.ItemType.Weapon:
                WeaponItem();
                break;
            case ItemData.ItemType.Shield:
                ShieldItem();
                break;
            case ItemData.ItemType.Mask:
                MaskItem();
                break;
            default:
                DefaultItem();
                break;
        }
    }
    private void DefaultItem()
    {
        if (onItemClick != null)
        {

            onItemClick();
        }
        if (!UiManager.UseMenu.activeSelf)
        {
            UiManager.UseMenu.SetActive(true);
            Debug.Log("bang!!");


            UiManager.UseButton.onClick.AddListener(data.UseItem);

            UiManager.ItemDescriptionButton.onClick.AddListener(data.DisplayDescription);
        }
        else
        {
            UiManager.UseMenu.SetActive(false);
        }


    }
    private void WeaponItem()
    {
        UiManager.GetUiManager().WeaponSlot.Equipped(this.data);
    }
    private void ShieldItem() {
        UiManager.GetUiManager().ShieldSlot.Equipped(this.data);

    }
    private void MaskItem()
    {
        UiManager.GetUiManager().MaskSlot.Equipped(this.data);

    }


}

