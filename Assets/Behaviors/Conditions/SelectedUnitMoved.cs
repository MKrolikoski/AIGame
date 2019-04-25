
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using System.Collections.Generic;
using UnityEngine;

[Condition("Custom/Conditions/SelectedUnitMoved")]
[Help("Check whether selected unit already moved")]
public class SelectedUnitMoved : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("SelectedUnitMoved: selectedUnit is null");
            return false;
        }
        return GameManager.instance.unitMoved;
    }
}