using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class FireBall : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        direction = Player.GetPlayer().transform.forward;
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
       
        Debug.Log("dafguq");
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(gameObject);
        }
    }
}
