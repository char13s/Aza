using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ShieldHitBox : MonoBehaviour
{
    [SerializeField] private GameObject farPoint;
    private List<Enemy> enemies = new List<Enemy>();
    public static event UnityAction<Enemy> punch;
    // Start is called before the first frame update
    private void OnDisable() {
        enemies.Clear();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")&& other.GetComponent<Enemy>()&&!enemies.Contains(other.GetComponent<Enemy>())) {
            Debug.Log("Enemy was deflected");
            if (punch != null) {
                punch(other.GetComponent<Enemy>());
            }
            enemies.Add(other.GetComponent<Enemy>());
          //other.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, farPoint.transform.position, 100 * Time.deltaTime);
          // other.gameObject.GetComponent<Enemy>().Hit = true;
        }
    }
}
