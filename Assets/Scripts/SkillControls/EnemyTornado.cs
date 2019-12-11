using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTornado : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        direction = -Mage.GetMage().transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, 20, 0, Space.Self);
        transform.position += direction * 4f * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            Mage.GetMage().CalculateAttack();
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
            
        }
        if (other.gameObject.CompareTag("Enviroment"))
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }


    }


}
