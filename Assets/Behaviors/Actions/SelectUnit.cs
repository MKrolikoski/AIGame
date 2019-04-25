using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

[Action("Custom/Actions/SelectUnit")]
[Help("Selects unit")]
public class SelectUnit : BasePrimitiveAction
{
    protected Type type = typeof(Unit);

    [OutParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        selectedUnit = findUnit();
        if(selectedUnit == null)
        {
            Debug.Log("Appropriate unit not found");
            return TaskStatus.FAILED;
        }
        SetUnit();
        return TaskStatus.COMPLETED;
    }

    protected virtual Unit findUnit()
    {
        Unit[] units = AI.instance.units.ToArray();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].GetType().IsSubclassOf(type) && units[i].canBeSelected)
            {
                return units[i];
            }
        }
        return null;
    }

    private void SetUnit()
    {
        GameManager.instance.SelectedUnit = selectedUnit;
    }

}
