using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class FireBall : MonoBehaviour
{
    bool setDirection;
    [SerializeField]private Vector3 direction;
    [SerializeField] private GameObject boom;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    private void Start() {
        
        if(!setDirection)
            direction =Player.GetPlayer().transform.forward;
        //LayerMask.GetMask("Ground");
        Destroy(gameObject, 9f);
        Debug.Log(direction);
    }
    // Update is called once per frame
    private void Update() {
        transform.position += speed * Time.deltaTime * direction;
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.gameObject.GetComponent<Enemy>() != null) {
            //other.gameObject.GetComponent<Enemy>().CalculateDamage(0,HitBoxType.Magic);}
        }
        if (other != null) {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
