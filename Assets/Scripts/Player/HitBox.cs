using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(effects, other.gameObject.transform);
            audio.PlayOneShot(hit);
            other.gameObject.GetComponent<Enemy>().CalculateDamage();
            //Debug.Log(other.gameObject.GetComponent<Enemy>().HealthLeft);
        }
    }
}
