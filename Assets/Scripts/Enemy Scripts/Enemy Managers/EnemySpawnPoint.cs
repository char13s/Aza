using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] float spawnTime;
    private void Start() {
        
    }
    private void OnEnable() {

    }
    public void Spawn() {
        StartCoroutine(DelaySpawn());
    }
    private void SpawnEnemy() {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
    IEnumerator DelaySpawn() {
        YieldInstruction wait = new WaitForSeconds(spawnTime);
        yield return wait;
        SpawnEnemy();
    }
}
