using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Mission1 : Objective
{
    [System.NonSerialized] private int conditions;
    public static UnityAction mission1Done;
    public static UnityAction mission1Update;

    public int Conditions { get => conditions; set { conditions = value; UiManager.GetUiManager().ObjectUpdate(); } }

    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
        Debug.Log("WOKE");
        mission1Done += MissionDone;
    }
    public override void Start()
    {
        base.Start();
        Debug.Log("Started");
    }
    // Update is called once per frame
    public override void Update()
    {
        
        ConditionControl();

    }
    private void ConditionControl() {
        
        if (IsActive)
        {
            switch (Conditions) {
                case 0:
                    
                    break;
                case 1:
                    Condition1();
                    break;


            }

            
            
        }

    }
  
    private void Condition1() {
        Debug.Log("um");
        if (mission1Update != null) {
            mission1Update();
        }
       
    }
    private void MissionDone() {
        Completed = true;
                IsActive = false;

    }
    

}
