using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Stando : MonoBehaviour
{
    private static Attack1Stando instance;
    public static Attack1Stando GetStando() => instance.GetComponent<Attack1Stando>();
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, 1f);
        Vector3 delta = -Player.GetPlayer().transform.forward;
        delta.y = 0;
        //transform.LookAt(Player.GetPlayer().transform);

        transform.rotation = Quaternion.LookRotation(delta);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
