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
        LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * 20 * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().CalculateDamage();
       
        
        
        }
        if (other != null) { 
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);}
        
    }
}
