using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZendsHair : MonoBehaviour
{
    [SerializeField] private Material demonHair;
    [SerializeField] private Material angelHair;
    [SerializeField] private Material baseHair;
    private Player pc;
    private void Awake() {
        
    }
    void Start()
    {
        pc = Player.GetPlayer();
       UiManager.angelSword += Saintity;
       UiManager.demonSword += Demonize;
        Player.formChange += ChangeHair;
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
    private void Base() {
        GetComponent<SkinnedMeshRenderer>().material = baseHair;
    }
    private void ChangeHair(int val) {
        switch (val) {
            case 0:
                Base();
                break;
            case 1:
                Demonize();
                break;
            case 2:
                Saintity();
                break;
        }
    }
}
