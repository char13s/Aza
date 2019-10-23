using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private enum ArrowType { Basic, Stun };
    [SerializeField ] private ArrowType type;
    [SerializeField] private float power;
    private Vector3 direction;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        direction = Player.GetPlayer().transform.forward;
        transform.rotation = Player.GetPlayer().transform.rotation;
        LayerMask.GetMask("Ground"); ;
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
        }
        if (other != null)
        {
            Debug.Log(other.name);
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
}
