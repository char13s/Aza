using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#pragma warning disable 0649
public class HitBox : MonoBehaviour
{
    private Player pc;
    [SerializeField] private AudioClip hit;
    [SerializeField] private GameObject effects;
    [SerializeField] private GameObject fire;
    private new AudioSource audio;
    private static HitBox instance;
    private GameObject enemyImAttacking;

    public GameObject EnemyImAttacking { get => enemyImAttacking; set => enemyImAttacking = value; }

    public static HitBox GetHitBox() => instance.GetComponent<HitBox>();


    // Start is called before the first frame update
    void Start()
    {
        pc = Player.GetPlayer();
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private Vector3 HitKnockback()
    {
        switch (pc.SkillId)
        {
            case 0:
                switch (KnockBackBehavior.HitId)
                {
                    case 1:
                        
                        return transform.forward * -1.3f;
                    case 2:
                        
                        return transform.forward * 2;
                    case 3:
                        
                        return transform.forward *15.5f;
                    case 4:
                        
                        return transform.forward + new Vector3(0, 10, 0);
                }
                return transform.forward + new Vector3(0, 0, 0);
            
            default: return transform.forward * -2;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            EnemyImAttacking = other.gameObject;
            Instantiate(effects, other.gameObject.transform);
            audio.PlayOneShot(hit);
            other.gameObject.GetComponent<Enemy>().CalculateDamage();
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().AddForce(HitKnockback(), ForceMode.VelocityChange);

            //Debug.Log(other.gameObject.GetComponent<Enemy>().HealthLeft);
        }
        if (other.gameObject.CompareTag("SlimeTree"))
        {
            Instantiate(fire, other.gameObject.transform);
            Destroy(other.gameObject, 4);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        EnemyImAttacking = null;
    }
}
