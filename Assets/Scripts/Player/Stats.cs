using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
[System.Serializable]
public class Stats
{
    //Variables
    private int health;
    private int attack;
    private int defense;
    
    private int mp;
    private int intellect;
    private int healthLeft;
    
    private int mpLeft;
    private byte level = 1;
    private int exp = 0;
    private int requiredExp;

	private int baseAttack;
	private int baseDefense;
	private int baseMp;
	private int baseHealth;

    private int attackBoost;
    private int defenseBoost;
    private int mpBoost;
    private int healthBoost;

    private int abilitypoints;
    //Events
    public static event UnityAction onHealthChange;
    public static event UnityAction onMPLeft;
    public static event UnityAction onLevelUp;
    public static event UnityAction onShowingStats;
	public static event UnityAction onBaseStatsUpdate;
	public static event UnityAction onObjectiveComplete;
    //Properties
    public int Health { get { return health; } set { health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return healthLeft; } set { healthLeft = Mathf.Clamp(value, 0, health);if (onHealthChange != null) { onHealthChange(); } } }
    public int MPLeft { get { return mpLeft; } set { mpLeft = Mathf.Clamp(value, 0, mp); if (onMPLeft != null) { onMPLeft(); } } }

    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int MP { get { return mp; } set { mp = value; } }
    public int Intellect { get { return intellect; } set { intellect = value; } }

    public byte Level { get => level; set => level = value; }
    public int Exp { get => exp; set { exp = value;  } }
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

    public int CalculateExpNeed() { int expNeeded = 4 * (Level * Level * Level); return  Mathf.Abs(Exp- expNeeded); }
    public int ExpCurrent() { return Exp - (4 * ((Level - 1) * (Level - 1) * (Level - 1))); }
    public void AddExp(int points)
    {
		exp += points;

    }
    /*void LevelUp(int points)
    {
        if (Level <= 20)
        {
            Health += 3;
            Attack += 2;
            Defense += 2;
            MP += 5;

        }
        if (Level > 20 && Level <= 50)
        {
            Health += 5;
            Attack += 3;
            Defense += 4;
            MP += 5;
        }
        if (Level > 50 && Level < 99)
        {
            Health += 7;
            Attack += 6;
            Defense += 6;
            MP += 5;
        }
        
    }*/
    
    public void DisplayAbilities()
    {
        if (onShowingStats != null)
        {
            onShowingStats();
        }
    }
    public void Start()
    {
        baseHealth = 22;
        healthLeft = baseHealth;
        baseMp = 10;
        mpLeft = baseMp;
        baseAttack = 10;
        baseDefense = 8;
        intellect = 6;
        level = 1;
        exp = 0;
        requiredExp = 50;
        SetStats();
        GameController.onGameWasStarted += UpdateUi;
        if (onHealthChange != null)
        {
            onHealthChange();
        }
        if (onMPLeft != null)
        {
            onMPLeft();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void UpdateUi() {
        if (onHealthChange != null)
        {
            onHealthChange();
        }
        if (onMPLeft != null)
        {
            onMPLeft();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void SetStats() {
        Attack = baseAttack + attackBoost;
        Defense = baseDefense + defenseBoost;
        MP = baseMp + mpBoost;
        Health = baseHealth + healthBoost;
    }
}
