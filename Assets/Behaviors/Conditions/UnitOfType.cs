
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/UnitOfType")]
[Help("Check whether unit is of specified type")]
public class UnitOfType : ConditionBase
{
    [InParam("UnitType")]
    public Unit unitType;

    [LocalParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        return selectedUnit.GetType() == unitType.GetType();
    }
}