using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    //Base value of the stat
    public int baseValue;
    //Value changed e.g. with items
    public int currentValue;
    //List of modifiers changing current value of the stat
    protected List<Modifier> modifiers; 

    #region currentValue getters\setters
    public int CurrentValue
    {
        get
        {
            int totalValue = currentValue;
            modifiers.ForEach(e => totalValue += e.modifierValue);
            return totalValue;
        }
        set
        {
            currentValue = value;
        }
    }
    #endregion

    public Stat(int baseValue)
    {
        modifiers = new List<Modifier>();
        this.baseValue = baseValue;
        CurrentValue = this.baseValue;
    }

    public void AddModifier(Modifier modifier)
    {
        if (modifier != null)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(Modifier modifier)
    {
        modifiers.Remove(modifier);
    }

    public int getValue()
    {
        int totalValue = currentValue;
        modifiers.ForEach(e => totalValue += e.modifierValue);
        if (totalValue < 0)
            return 0;
        return totalValue;
    }

    public virtual void OnNewTurn()
    {
        List<Modifier> modifiersToRemove = new List<Modifier>();
        modifiers.ForEach(m =>
        {
            if(!m.ReduceTimeLeft())
            {
                modifiersToRemove.Add(m);
            }
        });
        modifiersToRemove.ForEach(m => RemoveModifier(m));
    }
}
