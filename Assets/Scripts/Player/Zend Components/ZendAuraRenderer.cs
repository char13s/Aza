using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZendAuraRenderer : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer body;
    SkinnedMeshRenderer meshRef;
    [SerializeField]Player player;
    Quaternion qTo;
    // Start is called before the first frame update
    void Start() {

        meshRef = GetComponent<SkinnedMeshRenderer>();
        
    }
    private void Update() {
        transform.rotation = Quaternion.LookRotation(player.MainCam.transform.position-transform.position);
        
    }
    void Rotate() {
    
    }
}
