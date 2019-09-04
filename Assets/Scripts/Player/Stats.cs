using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class Stats
{
    //Variables
    private int health;
    private int attack;
    private int defense;
    private int stamina;
    private int intellect;
    private int healthLeft;
    private int staminaLeft;
    private byte level = 1;
    private int exp = 0;
    //Events
    public static event UnityAction onHealthChange;
    public static event UnityAction onStaminaChange;
    public static event UnityAction onLevelUp;
    public static event UnityAction onShowingStats;
    //Properties
    public int Health { get { return health; } set { health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return healthLeft; } set { healthLeft = Mathf.Clamp(value, 0, health);if (onHealthChange != null) { onHealthChange(); } if (healthLeft<=0) { Player.GetPlayer().Dead = true; }} }
    public int StaminaLeft { get { return staminaLeft; } set { staminaLeft = Mathf.Clamp(value, 0, stamina); if (onStaminaChange != null) { onStaminaChange(); } } }

    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Stamina { get { return stamina; } set { stamina = value; } }
    public int Intellect { get { return intellect; } set { intellect = value; } }

    public byte Level { get => level; set => level = value; }
    public int Exp { get => exp; set => exp = value; }

    public int CalculateExpNeed() { int expNeeded = 4 * (Level * Level * Level); return  Mathf.Abs(Exp- expNeeded); }
    public int ExpCurrent() { return Exp - (4 * ((Level - 1) * (Level - 1) * (Level - 1))); }
    public void AddExp(int points)
    {
        if (Level < 99)
        {
            Exp += points;

        }
        if (Exp >= CalculateExpNeed())
        {
            
            Level++;
            LevelUp(points);
            if (onLevelUp != null) { onLevelUp(); }
        }

    }
    void LevelUp(int points)
    {
        if (Level <= 20)
        {
            Health += 3;
            Attack += 2;
            Defense += 2;
            Stamina += 5;

        }
        if (Level > 20 && Level <= 50)
        {
            Health += 5;
            Attack += 3;
            Defense += 4;
            Stamina += 5;
        }
        if (Level > 50 && Level < 99)
        {
            Health += 7;
            Attack += 6;
            Defense += 6;
            Stamina += 5;
        }
        
    }
    public void DisplayAbilities()
    {
        if (onShowingStats != null)
        {
            onShowingStats();
        }
    }
    public void Start()
    {
        health = 22;
        healthLeft = health;
        stamina = 10;
        staminaLeft = stamina;
        attack = 6;
        defense = 5;
        intellect = 6;
        level = 1;
        exp = 0;
        GameController.onGameWasStarted += UpdateUi;
        if (onHealthChange != null)
        {
            onHealthChange();
        }
        if (onStaminaChange != null)
        {
            onStaminaChange();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void UpdateUi() {
        if (onHealthChange != null)
        {
            onHealthChange();
        }
        if (onStaminaChange != null)
        {
            onStaminaChange();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
}
