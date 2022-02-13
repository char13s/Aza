using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupsOfSpawners : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] spawnGroup;
    private bool spawn;

    public bool Spawn { get => spawn; set { spawn = value;if (spawn){ SpawnEnemies(); } } }

    private void SpawnEnemies() {
        Debug.Log("fuck you");
        foreach (EnemySpawnPoint esp in spawnGroup) {
            //esp.Spawn = true;
        }
    }
}
