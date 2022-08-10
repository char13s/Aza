using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerSpawnPoint : MonoBehaviour
{
    //needs a player ref
    private GameObject obj;
    [SerializeField] private GameObject player;
    public static event UnityAction<GameObject> targetMe;

    // Start is called before the first frame update
    void Start() {
        targetMe.Invoke(gameObject);
        // LevelManager.levelFinished += CreatePlayer;
        //
        StartCoroutine(DelaySpawn());
    }
    private void CreatePlayer(bool val) {

    }
    IEnumerator DelaySpawn() {
       YieldInstruction wait=new WaitForSeconds(1f);
        yield return wait;
        obj=Instantiate(player, transform.position, Quaternion.identity);
        targetMe.Invoke(obj);
    }
}
