using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectiveManager : MonoBehaviour
{
    private static ObjectiveManager instance;
    #region Instance Fields
    
    [SerializeField] private Objective mission1;
    
    #endregion

    #region Events
    public static UnityAction mission1Active;
     
    #endregion

    #region Getters and Setters
    public Objective Mission1 { get => mission1; set => mission1 = value; }
    #endregion


    public static ObjectiveManager GetObjectiveManager() => instance;
    // Start is called before the first frame update
    private void Awake()
    {
        mission1Active += Mission1Acvtive;
    }
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Mission1Acvtive() {
        mission1.IsActive = true;
        UiManager.GetUiManager().AddObjective(mission1);
        mission1.Intializing();
    }
}
