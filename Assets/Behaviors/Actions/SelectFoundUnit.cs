using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;
using System.Collections.Generic;

[Action("Custom/Actions/SelectFoundUnit")]
[Help("Selects found unit")]
public class SelectFoundUnit : BasePrimitiveAction
{
    protected Type type = typeof(Unit);

    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("Found unit is null");
            return TaskStatus.FAILED;
        }
        GameManager.instance.SelectedUnit = selectedUnit;
        selectedUnit.unitOwner.checkedUnits = new List<Unit>();
        //AI.instance.checkedUnits = new List<Unit>();
        return TaskStatus.COMPLETED;
    }
}
