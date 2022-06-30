using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPowers : EquipmentObj
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Circle() {
        //Everything in the area around you loses gravity function temporarily
    }
    public override void CircleReleased() {
        //pushes all objects in a certain area away
    }
    public override void UpCircle() {
        //Push all objects forward
    }
    public override void DownCircle() {
        //pull all objects towards
    }
}
