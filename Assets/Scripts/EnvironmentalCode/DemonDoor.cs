using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDoor : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject rightPoint;
    private int keys;

    public int Keys { get => keys; set { keys = value;if (keys == 4) { OpenTheDoors(); } } }

    private void Start() {
        Podium.lite += KeyUsed;
    }
    private void KeyUsed() {
        keys++;
    }
    private void OpenTheDoors() {
        leftDoor.transform.position = leftPoint.transform.position;
        rightDoor.transform.position = rightPoint.transform.position;
    }
}
