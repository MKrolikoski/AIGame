
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/IsUnitEssential")]
[Help("Check whether selected unit is essential")]
public class IsUnitEssential : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if(selectedUnit == null)
        {
            Debug.Log("IsUnitEssential: selectedUnit is null");
            return false;
        }
        return selectedUnit.isEssential;
    }
}