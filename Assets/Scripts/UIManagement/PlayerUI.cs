using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Text powerLv;
    // Start is called before the first frame update
    void Start() {
        Stats.onHealthChange += UpdateHealth;
        Stats.onMPLeft += UpdateMp;
        Stats.onPowerlv += UpdatePowerLv;
    }
    private void UpdateHealth() {
        healthBar.maxValue = Player.GetPlayer().stats.Health;
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        
    }
    private void UpdateMp() {
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
    }
    private void UpdatePowerLv(int val) {
        powerLv.text ="Power Lv.: "+ val.ToString();
    }
}
