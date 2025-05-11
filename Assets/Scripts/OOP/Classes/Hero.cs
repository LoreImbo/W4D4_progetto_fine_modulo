using System;
using UnityEngine;

[Serializable]
public class Hero
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int hp;
    [SerializeField]
    private Stats baseStats;
    [SerializeField]
    private ELEMENT resistance;
    [SerializeField]
    private ELEMENT weakness;
    [SerializeField]
    private Weapon weapon;

    public Hero(string name, int hp, Stats baseStats, ELEMENT resistance, ELEMENT weakness, Weapon weapon)
    {
        this.name = name;
        this.hp = hp;
        this.baseStats = baseStats;
        this.resistance = resistance;
        this.weakness = weakness;
        this.weapon = weapon;
    }

    public string GetName() => name;
    public int GetHp() => hp;
    public Stats GetBaseStats() => baseStats;
    public ELEMENT GetResistance() => resistance;
    public ELEMENT GetWeakness() => weakness;
    public Weapon GetWeapon() => weapon;

    public void SetName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            this.name = name;
    }
    public void SetHp(int hp) => this.hp = hp;
    public void SetBaseStats(Stats baseStats) => this.baseStats = baseStats;
    public void SetResistance(ELEMENT resistance) => this.resistance = resistance;
    public void SetWeakness(ELEMENT weakness) => this.weakness = weakness;
    public void SetWeapon(Weapon weapon) => this.weapon = weapon;

    public void AddHp(int amount)
    {
        SetHp(hp + amount); // this.SetHp(this.hp + amount); 
    }

    public void TakeDamage(int damage)
    {
        AddHp(-damage);
    }

    public bool IsAlive()
    {
        if (hp > 0)
            return true;
        return false;
    }
}
