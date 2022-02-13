using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
    }
    private void Update() {
        FacePlayer();
    }
    private void FacePlayer() { 
        Vector3 direction = player.MainCam.transform.position - transform.position;
        Quaternion qTo;
        qTo = Quaternion.LookRotation(direction);
        transform.rotation = qTo;
    }
}
