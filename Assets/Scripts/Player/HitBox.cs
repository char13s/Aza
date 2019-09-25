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
                switch (KnockBackBehavior.AnimationId)
                {
                    case 0:
                        Debug.Log("firstAttack");
                        return transform.forward *-10;
                    case 1:
                        Debug.Log("firstAttack2");
                        return transform.forward * 5;
                    case 2:
                        Debug.Log("Fly bitch2");
                        return transform.forward * 5;
                    case 3:
                        Debug.Log("Fly bitch3");
                        return transform.forward *15.5f;
                }
                return transform.forward + new Vector3(0, 0, 0);
            case 7:
                Debug.Log("wth");
                return transform.forward + new Vector3(0, 10, -3);
            default: return transform.forward + new Vector3(0, 0, -2.5f);
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
