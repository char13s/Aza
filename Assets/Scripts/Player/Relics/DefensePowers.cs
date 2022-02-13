using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DefensePowers : EquipmentObj
{

    public static event UnityAction<bool> defense;
    // Start is called before the first frame update
    void Start() {

    }
    public override void Circle() {
        Debug.Log("Defending");
        defense.Invoke(true);
        //first few frames perfect block trigger goes up but then turns off
        //the rest of block works long as theres mp left, meaning hurt box is turned off
        //so a second trigger box goes up that takes off 1 mp every hit
        //when mp is 0 and player still has block up player still gets hit
    }
    public override void CircleReleased() {
        defense.Invoke(false);
    }
}
