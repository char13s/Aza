using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LightBulbHolder : MonoBehaviour
{
    private Player player;
    private bool bulbObtained;
    private bool powered;
    [SerializeField] private LightBulb bulb;

    public bool Powered { get => powered; set { powered = value; if (powered) { LiteBulb(); } } }

    public static event UnityAction<int> removeBulb;
    // Start is called before the first frame update
    void Start()
    {
        Interactable.checkForBulb += ChechkForBulb;
        player = Player.GetPlayer();
    }
    private void ChechkForBulb(GameObject powerReciever) {
        Debug.Log("light bulb checked");
        if (powerReciever == gameObject && player.Bulbs > 0&&!bulbObtained) {
            bulbObtained = true;
            Debug.Log("light bulb active");
            bulb.gameObject.SetActive(true);
            if (removeBulb != null) {
                removeBulb(-1);
            }
        }
    }
    private void LiteBulb() {
        if (bulbObtained) {
            bulb.Lite = true;
        }
        else {
            Powered = false;
        }
        
    }
}
