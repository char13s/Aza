using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StatusEffects
{
    public enum Statuses { neutral, stunned, burned, frozen, gravityless, bleeding }
    private Statuses status;
    public static UnityAction onStatusUpdate;
    public Statuses Status { get => status; set { status = value; if(onStatusUpdate!=null) onStatusUpdate(); } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
