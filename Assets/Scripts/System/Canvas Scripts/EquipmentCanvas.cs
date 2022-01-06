using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCanvas : CanvasManager
{
    [SerializeField] private GameObject ListOfAbilites;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ListOfAbilityControl(bool val) => ListOfAbilites.SetActive(val);
}
