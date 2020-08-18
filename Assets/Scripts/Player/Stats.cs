using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
[System.Serializable]
public class Stats {
    //Variables
    private float health;
    private int attack;
    private int defense;

    private int mp;
    private int intellect;
    private float healthLeft;

    private int mpLeft;
    private byte level = 1;
    private float battlePower;
    private int requiredExp;

    private int baseAttack;
    private int baseDefense;
    private int baseMp;
    private int baseHealth;

    private int attackBoost;
    private int defenseBoost;
    private int mpBoost;
    private int healthBoost;

    private int swordLevel;
    private int demonFistLevel;
    private int swordProficency;

    private byte kryllLevel;

    private int abilitypoints;
    //Events
    public static event UnityAction onHealthChange;
    public static event UnityAction onMPLeft;
    public static event UnityAction onLevelUp;
    public static event UnityAction onShowingStats;
    public static event UnityAction onBaseStatsUpdate;
    public static event UnityAction onObjectiveComplete;
    //Properties
    public float Health { get { return health; } set { health = Mathf.Max(0, value); } }
    public float HealthLeft { get { return healthLeft; } set { healthLeft = Mathf.Clamp(value, 0, health); if (onHealthChange != null) { onHealthChange(); } } }
    public int MPLeft { get { return mpLeft; } set { mpLeft = Mathf.Clamp(value, 0, mp); if (onMPLeft != null) { onMPLeft(); } } }

    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int MP { get { return mp; } set { mp = value; } }
    public int Intellect { get { return intellect; } set { intellect = value; } }

    public byte Level { get => level; set => level = value; }
    public float BattlePower { get => battlePower; set { battlePower = value; UpdateUi(); } }
    public int BaseAttack { get => baseAttack; set { baseAttack = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseDefense { get => baseDefense; set { baseDefense = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseMp { get => baseMp; set { baseMp = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseHealth { get => baseHealth; set { baseHealth = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }

    public int AttackBoost { get => attackBoost; set { attackBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }
    public int DefenseBoost { get => defenseBoost; set { defenseBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }
    public int MpBoost { get => mpBoost; set { mpBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }
    public int HealthBoost { get => healthBoost; set { healthBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }

    public int RequiredExp { get => requiredExp; set => requiredExp = value; }
    public int Abilitypoints { get => abilitypoints; set { abilitypoints = value; if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }

    public int SwordProficency { get => swordProficency; set => swordProficency = value; }
    public int SwordLevel { get => swordLevel; set => swordLevel = value; }
    public byte KryllLevel { get => kryllLevel; set => kryllLevel = value; }

    public void DisplayAbilities() {
        if (onShowingStats != null) {
            onShowingStats();
        }
    }
    public void Start() {
        BattlePower = 10;
        baseHealth = (int)battlePower*2;
        healthLeft = baseHealth;
        baseMp = 15;
        mpLeft = baseMp;
        baseAttack = 3;
        baseDefense = 11;
        intellect = 6;
        level = 1;
        
        SwordLevel = 1;
        demonFistLevel = 1;
        requiredExp = 50;
        SetStats();
        Enemy.sendBP += AddBP;
        Player.weaponSwitch += SetStats;
        GameController.onGameWasStarted += UpdateUi;
        if (onHealthChange != null) {
            onHealthChange();
        }
        if (onMPLeft != null) {
            onMPLeft();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void UpdateUi() {
        if (onHealthChange != null) {
            onHealthChange();
        }
        if (onMPLeft != null) {
            onMPLeft();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void SetStats() {
        Attack = baseAttack + attackBoost+WeaponBoost();
        Defense = baseDefense + defenseBoost;
        MP = baseMp + mpBoost;
        Health = baseHealth + healthBoost;
    }
    private void AddBP(float powah) {
        BattlePower += powah;
        Debug.Log(powah);
    }
    private int WeaponBoost() {

        switch (Player.GetPlayer().Weapon) {
            case 0:
                return SwordLevel*2;
            case 1:
                return demonFistLevel*4;
            default:
                return 0;
        }
        
    }
}
