using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class WeakerHitBox : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private GameObject effects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {

            //GameObject burn=Instantiate(smallFire,other.transform);
            //Destroy(burn,3f);

            //EnemyImAttacking = other.gameObject;
            Instantiate(effects, other.gameObject.transform);
            //audio.PlayOneShot(hit);

            //other.GetComponent<NavMeshAgent>().enabled = false;

            Debug.Log("hit");
            if (other != null && other.GetComponent<Enemy>()) {
                enemies.Add(other.GetComponent<Enemy>());
                other.GetComponent<Enemy>().CalculateDamage(0);
                //other.GetComponent<Enemy>().KnockBack(HitKnockback());
                other.GetComponent<Enemy>().Grounded = false;
                GamePad.SetVibration(0, 0.2f, 0.2f);
                StartCoroutine(StopRumble());
            }
        }

        /*if (other.gameObject.CompareTag("SlimeTree"))
        {
            Instantiate(fire, other.gameObject.transform);
            Destroy(other.gameObject, 4);
        }*/
        if (other.gameObject.CompareTag("Dummy")) {

            //Instantiate(effects, other.gameObject.transform);
            other.GetComponent<Dummy>().Hit = true;
        }
    }
    private IEnumerator StopRumble() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        GamePad.SetVibration(0, 0, 0);
    }
}
