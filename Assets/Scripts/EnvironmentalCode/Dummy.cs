using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private bool hit;
    private Animator anim;
    private AudioClip bang;
    private AudioSource sound;
    public bool Hit { get => hit; set { hit = value;anim.SetBool("Hit",hit);if (hit) { StartCoroutine(HitCoroutine()); WasHit(); sound.PlayOneShot(bang);  } } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        bang = AudioManager.GetAudio().Bang;
    }
    private IEnumerator HitCoroutine () {
        
        yield return null;
        Debug.Log("hit is false");
        Hit = false;
        
    }
    private void WasHit() {

        Quaternion.LookRotation(Player.GetPlayer().transform.position,transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) {
            hit = true;

        }
    }

}
