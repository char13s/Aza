using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EndOfPortal : MonoBehaviour
{
    [SerializeField] private int level;
    public static event UnityAction<int> loadInLevel;
    private bool activated;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()&&!activated) {
            activated = true;
            loadInLevel.Invoke(level);
        //Destroy(gameObject);
        }
    }
}
