using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterData : MonoBehaviour
{
    private int level;

    public int Level { get => level; set { level = value;if (onLevel != null) { onLevel(); } } }
    private int battlePower;
    public static event UnityAction onLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
