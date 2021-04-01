using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControl : MonoBehaviour
{
    private Player pc;
    private AxisButton R2 = new AxisButton("R2");
    private void Awake() {
        pc = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.Flying) {
            if (Input.GetButton("R1")) {
                transform.position += new Vector3(0,3,0)*Time.deltaTime;
            }
            if (Input.GetAxis("R2")>0.5f) {
                Debug.Log("Fuck u");
                transform.position -= new Vector3(0, 3, 0) * Time.deltaTime;
            }
        }
    }
    
}
