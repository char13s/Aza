using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    private void Start()
    {
        direction = -Mage.GetMage().transform.forward;
        LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += direction * 20 * Time.deltaTime;
    }

    //private int AdditionPower()=>Player.GetPlayer().stats.Level 




    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Mage.GetMage().CalculateAttack();



        }
        if (other != null)
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
