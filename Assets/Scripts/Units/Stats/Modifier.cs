using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    //Value of modifier that affects stats
    public int modifierValue { get; private set; }
    //Time in turns in which the modifier effect wears off
    protected int modifierTimeInTurns;
    //Left time in turns of modifier's effect
    public int modifierLeftTimeInTurns { get; private set; }


    public Modifier(int modifierValue, int modifierTimeInTurns = 3)
    {
        this.modifierValue = modifierValue;
        this.modifierTimeInTurns = modifierTimeInTurns;
        modifierLeftTimeInTurns = modifierTimeInTurns;
    }

    public bool ReduceTimeLeft()
    {
        modifierLeftTimeInTurns--;
        if (modifierLeftTimeInTurns <= 0)
            return false;
        return true;
    }
}
