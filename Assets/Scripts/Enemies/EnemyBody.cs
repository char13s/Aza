using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Enemy body;

    private void OnTriggerEnter(Collider other) {
        body.HealthLeft--;
        body.Hit = true;
    }
}
