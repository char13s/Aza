using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable() {
        StartCoroutine(Spawn());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Spawn() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        while (isActiveAndEnabled) {
            yield return wait;
            Instantiate(fire, transform.position,Quaternion.identity);
        }
    }
}
