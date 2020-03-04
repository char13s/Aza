using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class Bed : MonoBehaviour {
    [SerializeField] GameObject emptyBed;
    [SerializeField] GameObject sleepBed;
    private bool sleeping;
    public static UnityAction bed;
    private void Awake() {
        UiManager.outaBed += NotSleep;
        UiManager.bedTime += Sleep;
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void NotSleep() {
        sleepBed.SetActive(false);
        sleeping = false;
        emptyBed.SetActive(true);
    }
    private void Sleep() {
        sleepBed.SetActive(true);
        emptyBed.SetActive(false);
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!sleeping) {
                if (Input.GetButtonDown("X")) {
                    if (bed != null) {
                        bed();
                    }
                    sleeping = true;

                }
            }
        }
    }
}
