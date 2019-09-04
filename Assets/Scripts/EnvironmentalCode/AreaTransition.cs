using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    
    [SerializeField] private GameObject nextArea;
    private bool traveling;
    private Coroutine waitCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {

            Player.GetPlayer().Nav.enabled = false;
            StartCoroutine(WaitCoroutine());
            

        }
    }
    private void NextScene() {

        Player.GetPlayer().transform.position = nextArea.transform.position;
    }
    private IEnumerator WaitCoroutine()
    {
        yield return null;
        NextScene();
        Player.GetPlayer().Nav.enabled = true;
    }
}
