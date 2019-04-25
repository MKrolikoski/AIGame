
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using System.Collections.Generic;
using UnityEngine;

[Condition("Custom/Conditions/SelectedUnitNotMoved")]
[Help("Check whether selected unit already moved")]
public class SelectedUnitNotMoved : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("SelectedUnitNotMoved: selectedUnit is null");
            return false;
        }
        return !GameManager.instance.unitMoved;
    }
}