using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class FireBall : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private GameObject boom;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    private void Start()
    {
        direction = Player.GetPlayer().transform.forward;
        LayerMask.GetMask("Ground");
        Destroy(gameObject, 9f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    //private int AdditionPower()=>Player.GetPlayer().stats.Level 



    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Enemy>() != null) { 
            other.gameObject.GetComponent<Enemy>().CalculateDamage(50f);}
       
        
        
        }
        if (other != null) { 
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);}
        
    }
}
