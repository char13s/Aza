using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyObjects : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hair;

    public GameObject Body { get => Body1; set => Body1 = value; }
    public GameObject Body1 { get => body; set => body = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
