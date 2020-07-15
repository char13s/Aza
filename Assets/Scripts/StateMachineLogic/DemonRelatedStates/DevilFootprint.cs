using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilFootprint : MonoBehaviour
{
    [SerializeField] private GameObject feetPowah;
    // Start is called before the first frame update
    void Start()
    {
        Player.flight += ActivateFoot;
    }
    private void ActivateFoot(bool val) {
        feetPowah.SetActive(val);
    }
}
