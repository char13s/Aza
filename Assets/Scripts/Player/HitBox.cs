using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;
#pragma warning disable 0649
public class HitBox : MonoBehaviour {
    private Player pc;
    
    
    [SerializeField] private GameObject effects;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject smallFire;
    [SerializeField] private GameObject farHitPoint;
    [SerializeField] private GameObject highHitPoint;
    private AudioSource audio;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject enemyImAttacking;

    public static UnityAction onEnemyHit;
    public static event UnityAction<AudioClip> sendsfx;
    public GameObject EnemyImAttacking { get => enemyImAttacking; set => enemyImAttacking = value; }
    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start() {
        pc = Player.GetPlayer();
        audio = pc.Sfx;
        
    }
    void OnEnable() {

        
        //audio.PlayOneShot(swing);
    }
    private void OnDisable() {

        enemies.Clear();
    }
    // Update is called once per frame
    void Update() {

    }
    //private Vector3 HitKnockback() {
    //    switch (pc.SkillId) {
    //        case 0:
    //
    //            switch (KnockBackBehavior.HitId) {
    //                case 1:
    //
    //                    return Player.GetPlayer().transform.forward * 2f;
    //                case 2:
    //
    //                    return Player.GetPlayer().transform.forward * -1.1f;
    //                case 3:
    //
    //                    
    //                case 4:
    //
    //                    return Player.GetPlayer().transform.forward + new Vector3(0, 5, 0);
    //
    //                case 5:
    //                    Debug.Log("fuck you slime");
    //                    return 
    //                case 6:
    //                    Debug.Log("fuck you slime");
    //                    return Player.GetPlayer().transform.forward * 1;
    //            }
    //            return transform.forward + new Vector3(0, 0, 0);
    //
    //        default: return Player.GetPlayer().transform.forward * -2;
    //    }
    //}
    private void Knockback(GameObject enemy) {

        switch (KnockBackBehavior.HitId) {
            case 0:
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, Player.GetPlayer().HitPoint.transform.position, 10 * Time.deltaTime);
                break;
            case 1:
                
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, farHitPoint.transform.position, 10 );//Vector3.MoveTowards(enemy.transform.position, farHitPoint.transform.position, 10 * Time.deltaTime); //;;(enemy.transform.position, farHitPoint.transform.position, 10 * Time.deltaTime
                break;
            case 2:
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, highHitPoint.transform.position, 10 * Time.deltaTime);
                break;
            
        }

    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy") && !enemies.Contains(other.gameObject)) {
            Instantiate(effects, other.gameObject.transform);

            if (other != null && other.GetComponent<Enemy>() && !enemies.Contains(other.gameObject)) {
                if (enemies.Contains(other.gameObject)) {
                    
                }

                if (onEnemyHit != null) {
                    onEnemyHit();
                }
                enemies.Add(other.gameObject);
                other.GetComponent<Enemy>().CalculateDamage(0);
                //other.GetComponent<Enemy>().KnockBack(HitKnockback());
                Knockback(other.gameObject);
                //other.GetComponent<Enemy>().Grounded = false;
                GamePad.SetVibration(0, 0.2f, 0.2f);
                StartCoroutine(StopRumble());
            }
        }

        if (other.gameObject.CompareTag("SlimeTree")) {
            //Instantiate(fire, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject, 2);
        }
        if (other.gameObject.CompareTag("Dummy")) {

            //Instantiate(effects, other.gameObject.transform);
            other.GetComponent<Dummy>().Hit = true;
        }
    }
    private IEnumerator StopRumble() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        GamePad.SetVibration(0, 0, 0);
    }
    private void OnTriggerExit(Collider other) {
        //EnemyImAttacking = null;
    }
}
