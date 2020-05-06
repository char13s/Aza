using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class SlimeSpawner : MonoBehaviour
{
    private static List<SlimeSpawner> slimeTrees = new List<SlimeSpawner>(10);
    [SerializeField] private Enemy slime;
    [SerializeField] private GameObject spawnPoint;
    private List<Enemy> spawnedEnemies;

    public List<Enemy> SpawnedEnemies { get => spawnedEnemies; set => spawnedEnemies = value; }

    // Start is called before the first frame update
    void Start()
    {
        SpawnedEnemies=new List<Enemy>();
        StartCoroutine(Spawn());
        Enemy.onAnyDefeated += RemoveTheDead;
        slimeTrees.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void RemoveTheDead(Enemy enemy)
    {

        SpawnedEnemies.Remove(enemy);
        
    }
    private Vector3 SpawnPointOffset(){

        switch (SpawnedEnemies.Count-1) {
            case 0:
                return new Vector3(1,0,0);
            case 1:
                return new Vector3(1, 0, 1);
            case 2:
                return new Vector3(0,0,1);
            case 3:
                return new Vector3(-1,0,0);
            case 4:
                return new Vector3(0,0,-1);
        }
        return new Vector3(0, 0, 0);
    }
    private IEnumerator Spawn()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(10f);
            

            if (SpawnedEnemies.Count < 2)
            {
                
                SpawnedEnemies.Add(Instantiate(slime, spawnPoint.transform.position+SpawnPointOffset(), Quaternion.identity).GetComponent<Slime>());
            }
        }
    }

}
