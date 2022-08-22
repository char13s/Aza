using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerSpawnPoint : MonoBehaviour
{
    //needs a player ref
    private GameObject obj;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnFire;
    [SerializeField] private GameObject boom;

    private bool spawn=true;
    public static event UnityAction<GameObject> targetMe;

    // Start is called before the first frame update
    void Start() {

        targetMe.Invoke(gameObject);
        // LevelManager.levelFinished += CreatePlayer;
        //
        StartCoroutine(DelaySpawn());
        StartCoroutine(ParticleSpawn());
    }
    IEnumerator DelaySpawn() {
       YieldInstruction wait=new WaitForSeconds(3f);
        yield return wait;
        obj=Instantiate(player, transform.position, Quaternion.identity);
        Instantiate(boom, transform.position, Quaternion.identity);
        targetMe.Invoke(obj);
        spawn = false;
    }
    IEnumerator ParticleSpawn() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        while (spawn) {
            yield return wait;
            Instantiate(spawnFire, transform.position, Quaternion.identity);
            
        }
    }
}
