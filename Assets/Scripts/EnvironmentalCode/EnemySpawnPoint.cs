using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private bool isQuestTriggered;

    private bool spawn;
    private bool spawned;
    private bool canSpawn;
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject spawnIn;

    public bool Spawn { get => spawn; set { spawn = value; } }

    private void Awake()
    {
        CinematicManager.cutsceneIsPlaying += CanSpawn;
        CinematicManager.cutsceneIsOver += CantSpawn;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn) { 
            if (Vector3.Distance(transform.position, Player.GetPlayer().transform.position) < 5&&!spawned)
            {
                SpawnEnemy();

            }
        }
        
       

    }
    private void CanSpawn() => StartCoroutine(WaitToSpawn());
    private void CantSpawn() => canSpawn = false;

    private IEnumerator WaitToSpawn() {
        YieldInstruction wait = new WaitForSeconds(1.2f);
        yield return wait;
        canSpawn = true;
    }
    private void SpawnEnemy()
    {
        spawned = true;
        Instantiate(spawnIn, transform);
        Instantiate(enemy, transform);
    }
}
