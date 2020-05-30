using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cinemachine;
public class Portal : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] private CinemachineVirtualCamera portalCam;
    private List<Enemy> spawnedEnemies;
    [SerializeField]private float spawnRate;
    [SerializeField] private Enemy enemy;
    public List<Enemy> SpawnedEnemies { get => spawnedEnemies; set => spawnedEnemies = value; }
    //public int Health { get => health; set { health = value;SetSlider(); if (health <= 0) { Dematerize(); } } }

    

    public int EnemyCount { get => enemyCount; set { enemyCount = value;if (enemyCount == 0 && spawned == maxEnemyNum) { Dematerize(); } } }

    [SerializeField] private GameObject despawn;
    [SerializeField]private GameObject spawnPoint;
    [SerializeField]private int eventNum;
    [SerializeField] private GameObject hit;
    [SerializeField] private Slider portalHealth;
    [SerializeField] private int maxEnemyNum;
    private int enemyCount;
    private int spawned;
    public static event UnityAction<int> sendEvent;
    private void Awake() {
        SpawnedEnemies = new List<Enemy>();
        StartCoroutine(Spawn());
        Enemy.onAnyDefeated += RemoveTheDead;
        //SetSlider();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void RemoveTheDead(Enemy enemy) {

        SpawnedEnemies.Remove(enemy);
        EnemyCount--;

    }
    private IEnumerator Spawn() {
        while (isActiveAndEnabled&&spawned<maxEnemyNum) {
            yield return new WaitForSeconds(spawnRate);


            if (SpawnedEnemies.Count < maxEnemyNum) {
                spawned++;
                EnemyCount++;
                SpawnedEnemies.Add(Instantiate(enemy, spawnPoint.transform.position , Quaternion.identity));
                Instantiate(despawn, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }
    private Vector3 SpawnPointOffset() {

        switch (SpawnedEnemies.Count - 1) {
            case 0:
                return new Vector3(5, 0, 0);
            case 1:
                return new Vector3(5, 0, 5);
            case 2:
                return new Vector3(0, 0, 5);
            case 3:
                return new Vector3(-5, 0, 0);
            case 4:
                return new Vector3(0, 0, -5);
        }
        return new Vector3(0, 0, 0);
    }
    //private void SetSlider() {
    //    portalHealth.value = Health;
    //    Debug.Log("Health go down");
    //}
    private void Dematerize() {
        Instantiate(despawn, transform.position,Quaternion.identity);
        if (sendEvent != null) {
            sendEvent(eventNum);
        }
        portalCam.m_Priority = 1000;
        Destroy(gameObject,2f);
    }
    //private void OnTriggerEnter(Collider other) {
    //    if (other.GetComponent<HitBox>()) {
    //        Instantiate(hit,transform.position+ SpawnPointOffset(), Quaternion.identity);
    //        Health--;
    //    }
    //}
}
