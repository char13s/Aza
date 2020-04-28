using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    private bool stamp;

    public bool Stamp { get => stamp; set { stamp = value;anim.SetBool("Stamp", stamp); } }
    private static Death instance;
    public static Death GetDeath()=>instance;
    // Start is called before the first frame update
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StopStamp.stopStamp += Stamping;
        EventManager.endOfDeathIntro += StartStamping;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Stamping() {
        Stamp = false;
    }
    private void StartStamping() {
        Stamp = true;
    }
}
