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
        CinematicManager.cutsceneIsPlaying += CantSpawn;
        CinematicManager.cutsceneIsOver +=CanSpawn;
        FreeFallZend.landed += SequenceSpawning;
        UiManager.portal += ResetSpwan;
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }
    private void OnEnable() {
        
    }

    // Update is called once per frame
    void Update()
    {
         
            if (Vector3.Distance(transform.position, Player.GetPlayer().transform.position) < 5&&!spawned)
            {
                SpawnEnemy();

            }
        
        
       

    }
    private void ResetSpwan(int c) {
        
        canSpawn = true;
        spawned = false;
    }
    private void SequenceSpawning(Vector3 loc,bool val) {
        CanSpawn();
    }
    private void CanSpawn() => canSpawn = true;
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
