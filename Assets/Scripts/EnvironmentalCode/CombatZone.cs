using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour {

    [SerializeField] private FireWalls[] walls;
    [SerializeField] private EnemySpawnPoint[] enemies;
    // Start is called before the first frame update
  
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            ActivateTheWalls();
            ActivateTheEnemies();
        }
    }
    private void ActivateTheWalls() {
        foreach (FireWalls wallie in walls) {
            wallie.Up = true;
        }
    }
    private void ActivateTheEnemies() {
        foreach(EnemySpawnPoint point in enemies){
            point.Spawn = true;
        }
    }
}
