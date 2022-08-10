using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidHitBox : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;
    private Coroutine hitCoroutine;
    private Coroutine repeatCoroutine;
    private Coroutine offCoroutine;
    private byte counter;
    // Start is called before the first frame update
    void Start() {
        hitCoroutine = StartCoroutine(HitCoroutine());

    }
    private void OnEnable() {
        hitCoroutine = StartCoroutine(HitCoroutine());
    }
    private void OnDisable() {
        counter = 0;
    }
    // Update is called once per frame
    void Update() {
        if (counter > 10) {
            StopCoroutine(RepeatCoroutine());
            StopCoroutine(HitCoroutine());
            
        }

    }
    private IEnumerator HitCoroutine() {
        YieldInstruction wait=new WaitForSeconds(0.05f);
        while (isActiveAndEnabled) {

            yield return wait;
            repeatCoroutine = StartCoroutine(RepeatCoroutine());
        }

    }
    private IEnumerator RepeatCoroutine() {
        YieldInstruction wait =new WaitForSeconds(0.01f);
        while (isActiveAndEnabled) {

            yield return wait;
            Debug.Log("ok what now");
            hitBox.SetActive(true);
            offCoroutine = StartCoroutine(OffCoroutine());
            counter++;
        }

    }
    private IEnumerator OffCoroutine() {

        
            yield return null;
            hitBox.SetActive(false);
            StopCoroutine(OffCoroutine());
       

    }
}
