using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrines : MonoBehaviour
{
    [SerializeField] private SpellTag relic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OpenPortal() {

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            if(Input.GetButtonDown("X")){
                Player.GetPlayer().items.AddRelic(relic);

            }
        }
    }
}
