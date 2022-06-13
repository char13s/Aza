using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZendHairControl : MonoBehaviour
{
    [SerializeField] private GameObject hair;
    [SerializeField] private Material normalState;
    [SerializeField] private Material energizedState;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputs.transformed += TransformControl;
    }
    private void TransformControl(bool val) {
        if (val) {
            hair.GetComponent<SkinnedMeshRenderer>().material = energizedState;
        }
        else {
            hair.GetComponent<SkinnedMeshRenderer>().material = normalState;
        }
    }
}
