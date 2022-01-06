using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class AbilityUI : MonoBehaviour
{
    [SerializeField] private EquipmentObj relic;
    private Button button;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetRelic);
    }
    private void SetRelic() {
        AbilityUIHolder.LastHolderSelected.Relic = relic;
        AbilityUIHolder.LastHolderSelected.Image.sprite = image.sprite;
    }
}
