using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [SerializeField] private GameObject electricField;
    [SerializeField] private Material unlit;
    [SerializeField] private Material lit;
    private bool lite;

    public bool Lite { get => lite; set { lite = value;if (lite) { LiteTheBulb(); } else { UnLightTheBulb(); } } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LiteTheBulb() {
        electricField.SetActive(true);
        GetComponent<MeshRenderer>().materials[1] = lit;
    }
    private void UnLightTheBulb() {
        electricField.SetActive(false);
        GetComponent<MeshRenderer>().materials[1] = unlit;
    }
}
