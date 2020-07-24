using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSword : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject despawn;
    private void OnEnable() {
        Instantiate(particles,transform);
    }
    private void OnDisable() {
        //Instantiate(despawn, transform);
    }
    
}
