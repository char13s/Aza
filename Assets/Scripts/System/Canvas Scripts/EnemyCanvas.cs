using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] Slider hpSlider;
    [SerializeField] Image fillRef;
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
    public void SetEnemyHealth() {print(hpSlider.value);
            print(hpSlider.maxValue);
            print(hpSlider.maxValue / 4);
        if (hpSlider.value < (hpSlider.maxValue / 4)) {
            
            fillRef.color = Color.yellow;
            print("Yellow health");
        }
        //fillRef.color = Color.green;
        print("Has been updated");
    }
}
