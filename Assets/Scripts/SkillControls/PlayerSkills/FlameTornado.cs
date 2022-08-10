using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class FlameTornado : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField]private GameObject boom;
    [SerializeField] private ParticleSystem tornado;
    private int timer;
    // Start is called before the first frame update
    void Start()
    {
        direction = Player.GetPlayer().transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        transform.Rotate(0, 20, 0, Space.Self);
        transform.position += direction * 2.5f * Time.deltaTime;
        if (timer == 180) {
            Defuse();
        }
    }
    private void Defuse() {

        tornado.Stop();
        Destroy(gameObject, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            //other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            
            Instantiate(boom, transform.position, transform.rotation);
            //tornado.Stop();
            
            //Destroy(gameObject);
            if (other != null && other.GetComponent<Enemy>())
            {

            }
            }
        if (other.gameObject.CompareTag("Enviroment"))
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }


        }
    
    
}
