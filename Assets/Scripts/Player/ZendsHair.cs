using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZendsHair : MonoBehaviour
{
    [SerializeField] private Material demonHair;
    [SerializeField] private Material angelHair;
    // Start is called before the first frame update
    void Start()
    {
       UiManager.angelSword += Saintity;
       UiManager.demonSword += Demonize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Demonize() {
        GetComponent<SkinnedMeshRenderer>().material = demonHair;
    }
    private void Saintity() {
        GetComponent<SkinnedMeshRenderer>().material = angelHair;
    }
}
