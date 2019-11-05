using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class FlameTornado : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField]private GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        direction = Player.GetPlayer().transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, 20, 0, Space.Self);
        transform.position += direction * 2.5f * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().CalculateDamage();
            other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            other.GetComponent<Rigidbody>().AddForce(transform.forward + new Vector3(0, 5, 0), ForceMode.VelocityChange);
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
