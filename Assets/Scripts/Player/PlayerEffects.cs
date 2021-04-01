using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] Material auraEffect;
    [SerializeField] private GameObject chargeAura;
    private SkinnedMeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        Player.auraUp += ActivateAura;
        Player.charge += Charge;
    }
    private void ActivateAura(bool val) {
        if (val) {
            mesh.material = auraEffect;
            
        }
        else {
            print("drop aura");
            mesh.material = null;
        }
    }
    private void Charge(bool val) {
        chargeAura.SetActive(val);
    }
}
