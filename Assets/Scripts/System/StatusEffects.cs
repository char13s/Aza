using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects 
{
    private enum Statuses {neutral,stunned,burned,frozen,gravityless,bleeding }
    private Statuses status;

    private Statuses Status { get => status; set => status = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
