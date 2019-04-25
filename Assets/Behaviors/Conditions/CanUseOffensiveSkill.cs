
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/CanUseOffensiveSkill")]
[Help("Check whether selected unit can use offensive skill")]
public class CanUseOffensiveSkill : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("CanUseOffensiveSkill: selectedUnit is null");
            return false;
        }
        return selectedUnit.skills.CanUseOffensiveSkill();
    }
}