using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Enemy body;

    public static event UnityAction<float> force;
    private void OnTriggerEnter(Collider other) {
        body.HealthLeft--;
        body.Hit = true;
        if (force != null) {
            force(50);
        }
    }
}
