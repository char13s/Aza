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

    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemies=new List<Enemy>();
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

        spawnedEnemies.Remove(enemy);
        
    }
    private IEnumerator Spawn()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(10f);
            

            if (spawnedEnemies.Count < 5)
            {
                
                spawnedEnemies.Add(Instantiate(slime, spawnPoint.transform.position, Quaternion.identity).GetComponent<Slime>());
            }
        }
    }

}
