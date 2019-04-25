using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPoints : Stat
{

    //Event fired when current mp is changed
    public event System.Action<int, int> OnManaChanged;

    public Stat manaRegen { get; private set; }

    public ManaPoints(int baseValue, int manaRegen = 15) : base(baseValue)
    {
        this.manaRegen = new Stat(manaRegen);
    }

    public void LoseMana(int mp)
    {
        mp = Mathf.Clamp(mp, 0, CurrentValue);
        CurrentValue -= mp;

        if (OnManaChanged != null)
        {
            OnManaChanged(baseValue, CurrentValue);
        }
    }

    public void GainMana(int mp)
    {
        mp = Mathf.Clamp(mp, 0, baseValue - CurrentValue);
        CurrentValue += mp;
        if (OnManaChanged != null)
        {
            OnManaChanged(baseValue, CurrentValue);
        }
    }

    public override void OnNewTurn()
    {
        base.OnNewTurn();
        GainMana(manaRegen.getValue());
    }
}
