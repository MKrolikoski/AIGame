using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("Custom/Actions/UseOffensiveSkill")]
[Help("Selected unit uses offensive skill")]
public class UseOffensiveSkill : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("UseOffensiveSkill: selectedUnit is null");
            return TaskStatus.FAILED;
        }

        selectedUnit.skills.UseOffensiveSkill();

        return TaskStatus.COMPLETED;
    }

}
