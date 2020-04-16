using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        HoldAndRelease.destroy += ByeBYe;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ByeBYe() {
        Destroy(gameObject,0.1f);
    }
}
