using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private enum ArrowType { Basic, Stun };
    [SerializeField] private ArrowType type;
    [SerializeField] private float power;
    [SerializeField] private float speed;
    [SerializeField] private GameObject fire;
    private Vector3 direction;
    private Player pc;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    
    void Start()
    {
        pc = Player.GetPlayer();
        direction = pc.ArrowPoint.transform.forward;
        transform.rotation = pc.ArrowPoint.transform.rotation;
        LayerMask.GetMask("Ground");
        Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetPlayer().LockedOn) {
            transform.position = Vector3.MoveTowards(transform.position, pc.BattleMode.EnemyTarget.transform.position, 30 * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(transform.position - pc.BattleMode.EnemyTarget.transform.position);
        }
        else {
            transform.position += direction * speed * Time.deltaTime;

        }
        
    }
    private void ArrowEffects(Enemy target) {

        switch (type) {
            case ArrowType.Basic:
                break;
            case ArrowType.Stun:
                target.status.Status = StatusEffects.Statuses.stunned;
                Debug.Log("CONO DIO DA");
                break;


        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().CalculateDamage(power);
            ArrowEffects(other.gameObject.GetComponent<Enemy>());
        }
        if (other != null)
        {
            GetComponent<Rigidbody>().useGravity = false;
            
            
            Instantiate(boom, transform.position, transform.rotation);
            //Instantiate(fire, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
}
