using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doppleganger : EquipmentObj
{
    [SerializeField] private GameObject dopple;
    private GameObject summoning;
    private bool summoned;
    public override void Circle() {
        SummonDopple();
    }

    public override void CircleReleased() {

    }

    public override void DownCircle() {

    }

    public override void UpCircle() {

    }
    private void SummonDopple() {
        print("Dopple summoned");
        if (summoned) {
            
        }
        else
            summoning=Instantiate(dopple, Player.GetPlayer().transform.position + new Vector3(2, 0, 0), Quaternion.identity);
    }
}
