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
    private void Start() {
        //direction = Player.GetPlayer().transform.forward;
        //LayerMask.GetMask("Ground");
        Destroy(gameObject, 9f);
    }

    // Update is called once per frame
    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy")) {
            if (other.gameObject.GetComponent<Enemy>() != null) {
                //other.gameObject.GetComponent<Enemy>().CalculateDamage(0,HitBoxType.Magic);}
            }
            if (other != null) {
                Instantiate(boom, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
    }
}
