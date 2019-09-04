using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;
    private Coroutine hitCoroutine;
    private Coroutine repeatCoroutine;
    private Coroutine offCoroutine;
    private byte counter;
    // Start is called before the first frame update
    void Start()
    {
        hitCoroutine = StartCoroutine(HitCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 10)
        {
            StopCoroutine(RepeatCoroutine());
            StopCoroutine(HitCoroutine());
            Destroy(gameObject);
        }

    }
    private IEnumerator HitCoroutine()
    {

        while (isActiveAndEnabled)
        {

            yield return new WaitForSeconds(2);
            repeatCoroutine = StartCoroutine(RepeatCoroutine());
        }

    }
    private IEnumerator RepeatCoroutine()
    {

        while (isActiveAndEnabled)
        {

            yield return new WaitForSeconds(0.3f);
            hitBox.SetActive(true);
            offCoroutine = StartCoroutine(OffCoroutine());
            counter++;
        }

    }
    private IEnumerator OffCoroutine()
    {

        while (isActiveAndEnabled)
        {

            yield return null;
            hitBox.SetActive(false);
            StopCoroutine(OffCoroutine());
        }

    }
}
