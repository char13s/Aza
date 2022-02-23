using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnEnable() {
        
    }
    public void SpawnEnemy() {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
    
}
