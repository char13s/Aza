using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        direction = AzaAi.GetAza().transform.forward;
        transform.rotation= AzaAi.GetAza().transform.rotation; ;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * 20 * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().CalculateDamage();


            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
