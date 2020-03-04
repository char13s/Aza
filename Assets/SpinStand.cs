using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinStand : MonoBehaviour
{
    private void Awake() {
        //AirSpin.destroyReminants += WhenToDestroy;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,0.5f);
        Vector3 delta = -Player.GetPlayer().transform.forward;
        delta.y = 0;
        //transform.LookAt(Player.GetPlayer().transform);

        transform.rotation = Quaternion.LookRotation(delta);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void WhenToDestroy() {
        if (gameObject != null) {
            
        }
    }
}
