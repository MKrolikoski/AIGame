using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

[Action("Custom/Actions/FindAnyUnit")]
[Help("Finds any unit")]
public class FindAnyUnit : BasePrimitiveAction
{
    [OutParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        selectedUnit = FindEssentialUnit();
        if (selectedUnit == null)
            selectedUnit = FindNotCheckedUnit();
        if (selectedUnit == null)
        {
            Debug.Log("No unit was found");
            return TaskStatus.FAILED;
        }
        return TaskStatus.COMPLETED;
    }

    private Unit FindNotCheckedUnit()
    {
        Unit[] units = AI.instance.units.ToArray();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].canBeSelected && !AI.instance.checkedUnits.Contains(units[i]))
            {
                return units[i];
            }
        }
        return FindUnit();
    }

    private Unit FindUnit()
    {
        Unit[] units = AI.instance.checkedUnits.ToArray();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].canBeSelected)
            {
                return units[i];
            }
        }
        return null;
    }

    private Unit FindEssentialUnit()
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
