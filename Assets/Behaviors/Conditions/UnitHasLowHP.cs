
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/UnitHasLowHP")]
[Help("Check whether unit has hp lower than specified threshold")]
public class UnitHasLowHP : ConditionBase
{
    [InParam("Unit")]
    public Unit unit;

    [InParam("HpThreshold (%)", DefaultValue = 0.3)]
    public float hpThreshold;

    public override bool Check()
    {
        if (unit == null)
        {
            Debug.Log("UnitHasLowHP: selectedUnit is null");
            return false;
        }
        float unitPercHp = unit.stats.hp.getValue() / (float)unit.stats.hp.baseValue;
        return unitPercHp < hpThreshold;
    }
}