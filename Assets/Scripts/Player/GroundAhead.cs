using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAhead : MonoBehaviour
{
    private Player pc;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == null && !other.gameObject.CompareTag("Player"))
        {
            pc.Wall = true;

        }
        //else { pc.wall = false; }

    }
    void OnTriggerExit(Collider other)
    {
        //pc.wall = false;

    }
}
