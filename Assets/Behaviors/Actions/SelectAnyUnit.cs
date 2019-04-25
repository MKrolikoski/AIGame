using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

[Action("Custom/Actions/SelectAnyUnit")]
[Help("Selects any unit")]
public class SelectAnyUnit : BasePrimitiveAction
{
    [OutParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        selectedUnit = findEssentialUnit();
        if (selectedUnit == null)
            selectedUnit = findAnyUnit();
        if (selectedUnit == null)
        {
            Debug.Log("No unit was found");
            return TaskStatus.FAILED;
        }
        GameManager.instance.SelectedUnit = selectedUnit;
        return TaskStatus.COMPLETED;
    }

    private Unit findAnyUnit()
    {
        Unit[] units = AI.instance.units.ToArray();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].canBeSelected)
            {
                return units[i];
            }
        }
        return null;
    }

    private Unit findEssentialUnit()
    {
        Unit[] units = AI.instance.units.ToArray();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].isEssential && units[i].canBeSelected)
            {
                return units[i];
            }
        }
        return null;
    }
}
