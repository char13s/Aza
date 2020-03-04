using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reminant : MonoBehaviour
{
    [SerializeField] private GameObject boom;
    [SerializeField] private bool freefall;
    // Start is called before the first frame update
    
    void Start()
    {
        Instantiate(boom, transform);
        Destroy(gameObject,0.2f);
        if (freefall) {
             Vector3 delta = -FreeFallZend.GetFreeFallingZend().transform.forward;
            delta.y = 0;
            delta.x = 270;
            transform.rotation = Quaternion.LookRotation(delta);

        }
        else {
           Vector3 delta = -Player.GetPlayer().transform.forward;
            delta.y = 0;
            transform.rotation = Quaternion.LookRotation(delta);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
