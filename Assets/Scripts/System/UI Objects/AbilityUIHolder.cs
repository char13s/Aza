using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class AbilityUIHolder : MonoBehaviour
{
    private EquipmentObj relic;
    private Image image;
    private static AbilityUIHolder lastHolderSelected;
    public EquipmentObj Relic { get => relic; set => relic = value; }
    public static AbilityUIHolder LastHolderSelected { get => lastHolderSelected; set => lastHolderSelected = value; }
    public Image Image { get => image; set => image = value; }

    private void Start() {
        Image = GetComponent<Image>();
    }
    public void SetSlot() {
        LastHolderSelected = this;
    }
}
