using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("Custom/Actions/UseDefensiveSkill")]
[Help("Selected unit uses defensive skill")]
public class UseDefensiveSkill : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("UseDefensiveSkill: selectedUnit is null");
            return TaskStatus.FAILED;
        }

        selectedUnit.skills.UseDefensiveSkill();

        return TaskStatus.COMPLETED;
    }

}
