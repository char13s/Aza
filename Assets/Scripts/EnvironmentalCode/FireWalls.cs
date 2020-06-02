using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWalls : MonoBehaviour
{
    [SerializeField] private GameObject fireWall;
    [SerializeField] private GameObject despawn;
    private bool up;

    public bool Up { get => up; set { up = value;if (up) { Activate(); } } }

    private void Activate() {
        fireWall.SetActive(true);
    }
    private void Deactivate() {
        fireWall.SetActive(false);
        Instantiate(despawn,transform);
    }
}
