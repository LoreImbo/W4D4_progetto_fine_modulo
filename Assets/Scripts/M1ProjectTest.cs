using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [SerializeField]
    private Hero a;
    [SerializeField]
    private Hero b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Combattimento(a, b);
    }

    void Combattimento(Hero a, Hero b)
    {
        if (!a.IsAlive() || !b.IsAlive())
            return;
        if (ControlloSpd(a, b))
        {
            InterazioneAttacco(a, b);
            if (b.IsAlive())
                InterazioneAttacco(b, a);
        }
        else
        {
            InterazioneAttacco(b, a);
            if (a.IsAlive())
                InterazioneAttacco(a, b);
        }

    }

    bool ControlloSpd(Hero a, Hero b)
    {   
        if (Stats.Sum(a.GetBaseStats(), a.GetWeapon().GetBonusStats()).spd >= Stats.Sum(b.GetBaseStats(), b.GetWeapon().GetBonusStats()).spd)
            return true;
        return false;
    }

    void InterazioneAttacco(Hero a, Hero b)
    {
        Debug.Log($"L'eroe {a.GetName()} sta attaccando, mentre l'eroe {b.GetName()} sta difendendo");
        if (GameFormulas.HasHit(a.GetBaseStats(), b.GetBaseStats()))
        {
            if (GameFormulas.HasElementAdvantage(a.GetWeapon().GetElem(), b))
                Debug.Log("WEAKNESS");
            else if (GameFormulas.HasElementDisadvantage(a.GetWeapon().GetElem(), b))
                Debug.Log("RESIST");

            int baseDmg = GameFormulas.CalculateDamage(a, b);
            Debug.Log($"Il danno dell'attaccante è {baseDmg}");
            b.TakeDamage(baseDmg);

            if (!b.IsAlive())
                Debug.Log($"L'eroe {a.GetName()} è il vincitore");
        } 
        else
            Debug.Log("Attacco mancato");
            
    }
}
