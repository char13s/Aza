using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private GameObject interactIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerIsInRange();
    }
    private float Distance => Vector3.Distance(Player.GetPlayer().transform.position, transform.position);
    private void PlayerIsInRange()
    {
        if (Distance < 10)
        {
            interactIcon.SetActive(true);
            transform.LookAt(Player.GetPlayer().transform.position);
            if (Input.GetButtonDown("Square"))
            {
                if (Dialogue.dialogueUp != null)
                {
                    Dialogue.dialogueUp();
                }
            }
        }
        else
        {
            interactIcon.SetActive(false);
        }

    }
}
