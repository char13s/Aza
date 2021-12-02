using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowers : DarkPowerSet
{
    public override void Triangle() {
        base.Triangle();
        Anim.SetTrigger("DarkForcePush");
    }
}
