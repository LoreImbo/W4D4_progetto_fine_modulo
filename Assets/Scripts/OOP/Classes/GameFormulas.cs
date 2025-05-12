
using UnityEngine;

public static class GameFormulas
{
    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetWeakness();
    }
    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetResistance();
    }
    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender))
            return 1.5f;
        else if (HasElementDisadvantage(attackElement, defender))
            return 0.5f;
        else
            return 1f;
    }

    public static bool HasHit(Stats attacker, Stats defender)
    {
        int hitChance = attacker.aim - defender.eva;
        if (Random.Range(0,100) > hitChance)
        {
            Debug.Log("MISS");
            return false;
        }
        else
            return true;
    }

    public static bool IsCrit(int critValue)
    {
        if (Random.Range(0,100) < critValue)
        {
            Debug.Log("CRIT");
            return true;
        }
        else
            return false;
    }

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats sumStatsAttacker = Stats.Sum(attacker.GetBaseStats(), attacker.GetWeapon().GetBonusStats());
        Stats sumStatsDefender = Stats.Sum(defender.GetBaseStats(), defender.GetWeapon().GetBonusStats());

        int defenseDefender = 0;
        if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.PHYSICAL)
            defenseDefender = sumStatsDefender.def;
        else if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.MAGICAL)
            defenseDefender = sumStatsDefender.res;

        int baseDmg = sumStatsAttacker.atk - defenseDefender;

        baseDmg = Mathf.RoundToInt(baseDmg*EvaluateElementalModifier(attacker.GetWeapon().GetElem(), defender));

        if (IsCrit(baseDmg))
            baseDmg *= 2;

        return Mathf.Max(0,baseDmg);
    }
}
