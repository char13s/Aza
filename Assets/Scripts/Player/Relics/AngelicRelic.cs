using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AngelicRelic : EquipmentObj
{
    public static event UnityAction teleportTo;
    public static event UnityAction quickDodge;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Circle() {
        quickDodge.Invoke();
    }
    public override void UpCircle() {
        teleportTo.Invoke();
        print("boom");
    }
}
