using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Enemy body;

    public static event UnityAction<float> force;
    private void OnTriggerEnter(Collider other) {
        body.CalculateDamage(1);
        print("Hit");
        body.Hit = true;
        if (force != null) {
            force(50);
        }
        //body.KnockedUp();
    }
}
