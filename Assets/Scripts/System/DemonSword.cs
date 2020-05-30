using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSword : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    private void OnEnable() {
        Instantiate(particles,transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
