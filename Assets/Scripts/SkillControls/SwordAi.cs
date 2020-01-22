using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SwordAi : MonoBehaviour
{
    public static event UnityAction teleport;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,Player.GetPlayer().BattleMode.EnemyTarget.transform.position,30*Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(transform.position - Player.GetPlayer().BattleMode.EnemyTarget.transform.position);
    }
    private IEnumerator Wait() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Destroy(gameObject,2f);
            Player.GetPlayer().Nav.enabled=false;
            Player.GetPlayer().transform.position = transform.position;
        }
    }
}
