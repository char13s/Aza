using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSpawner : MonoBehaviour
{
    [SerializeField] private GameObject slash;
    private void OnEnable() {
        SpawnSlash();
    }
    // Start is called before the first frame update
    void Start() {
        //StartCoroutine(SpawnAfterTime());
    }

    // Update is called once per frame
    void Update() {
        //transform.Rotate(new Vector3(0, 0, 1));
    }
    IEnumerator SpawnAfterTime() {
        YieldInstruction wait = new WaitForSeconds(1);
        while(isActiveAndEnabled) { 
        yield return wait;
       
        }
    }
    public void SpawnSlash() { 
        Instantiate(slash,transform.position,transform.rotation);
    }
}
