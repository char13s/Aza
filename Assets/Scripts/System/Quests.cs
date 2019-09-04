using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    private GameObject QuestBoard;
    // Start is called before the first frame update
    void Start()
    {
        QuestBoard = GameObject.Find("Zend/Camera/Canvas/QuestBoard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.Z)&&!QuestBoard.activeSelf) {
                QuestBoard.SetActive(true);
                return;
            }
            if (Input.GetKeyDown(KeyCode.Z) && QuestBoard.activeSelf) {
                QuestBoard.SetActive(false);
                return;

            }
            

        }
    }
}
