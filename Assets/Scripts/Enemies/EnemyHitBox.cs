using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class EnemyHitBox : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    [SerializeField] private GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Instantiate(effect, transform.position, Quaternion.identity);
            if (player.Guard)
            {
                enemy.CalculateAttack();
            }
            else
            {
                enemy.CalculateAttack();
                player.Hit = true;
            }
            

        }
    }
}
