
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/CanUseDefensiveSkill")]
[Help("Check whether selected unit can use defensive skill")]
public class CanUseDefensiveSkill : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("CanUseDefensiveSkill: selectedUnit is null");
            return false;
        }
        return selectedUnit.skills.CanUseDefensiveSkill();
    }
}