using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AngelicRelic : EquipmentObj
{
    public static event UnityAction lightSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Circle() {
        
    }
    public override void UpCircle() {
        lightSpeed.Invoke();
        print("boom");
    }
}
