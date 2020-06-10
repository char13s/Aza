using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#pragma warning disable 0649
public class EnemyHitBox : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    [SerializeField] private GameObject effect;
    private bool occured;
    public static event UnityAction hit;
    public static event UnityAction guardHit;
    private void OnDisable() {
        occured = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Instantiate(effect, transform.position, Quaternion.identity);
            if (player.Guard)
            {
                if (!occured) {
                    if (guardHit != null) {
                        guardHit();
                    }
                    enemy.HitGuard();
                    occured = true;
                }
            }
            else
            {
                if (!occured) { 
                if (hit != null) {
                    hit();
                }
                    enemy.CalculateAttack();
                    occured = true;
                    
                }
            }
        } 
    }
}
