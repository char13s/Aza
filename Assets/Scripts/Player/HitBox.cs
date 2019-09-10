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
    
    private new AudioSource audio;

    

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
    private Vector3 HitKnockback() {
        switch (pc.SkillId) {
            case 7:
                Debug.Log("wth");
                return transform.forward + new Vector3(0,10,-3);
            default: return transform.forward+new Vector3(0, 0, -5);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(effects, other.gameObject.transform);
            audio.PlayOneShot(hit);
            other.gameObject.GetComponent<Enemy>().CalculateDamage();
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().AddForce(HitKnockback(), ForceMode.VelocityChange);
            
            //Debug.Log(other.gameObject.GetComponent<Enemy>().HealthLeft);
        }
    }
}
