using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallDetector : MonoBehaviour
{
    [SerializeField] private float colliderNumber;
    public static UnityAction<bool> up;
    public static UnityAction<bool> down;
    public static UnityAction<bool> left;
    public static UnityAction<bool> right;

    private void OnTriggerEnter(Collider other) {
        switch (colliderNumber) {
            case 0:
                if (up != null) {
                    up(true);
                }
                break;
            case 1:
                if (down != null) {
                    down(true);
                }
                break;
            case 2:
                if (left != null) {
                    left(true);
                }
                break;
            case 3:
                if (right != null) {
                    right(true);
                }
                break;
        }
    }
    private void OnTriggerExit(Collider other) {
        switch (colliderNumber) {
            case 0:
                if (up != null) {
                    up(false);
                }
                break;
            case 1:
                if (down != null) {
                    down(false);
                }
                break;
            case 2:
                if (left != null) {
                    left(false);
                }
                break;
            case 3:
                if (right != null) {
                    right(false);
                }
                break;
        }
    }
}
