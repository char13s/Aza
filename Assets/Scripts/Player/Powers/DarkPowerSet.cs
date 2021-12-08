using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPowerSet : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public Animator Anim { get => anim; set => anim = value; }

    public virtual void Triangle() { 
        
    }
    public virtual void TriangleUp() { 
    
    }
    public virtual void TriangleDown() { 
    
    }
}
