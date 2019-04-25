using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("Custom/Actions/UseOffensivePotion")]
[Help("Selected unit uses offensive potion")]
public class UseOffensivePotion : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("UseOffensivePotion: selectedUnit is null");
            return TaskStatus.FAILED;
        }

        selectedUnit.inventory.UseOffensivePotion();

        return TaskStatus.COMPLETED;
    }

}
