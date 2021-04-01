using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zends;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateAllZendSkins(Material mat) {
        foreach (GameObject zend in zends) {
            GetComponentInChildren<SkinnedMeshRenderer>().material = mat;
        }
    }
}
