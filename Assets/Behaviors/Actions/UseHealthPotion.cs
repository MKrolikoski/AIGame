using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("Custom/Actions/UseHealthPotion")]
[Help("Selected unit uses health potion")]
public class UseHealthPotion : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("UseHealthPotion: selectedUnit is null");
            return TaskStatus.FAILED;
        }

        selectedUnit.inventory.UseHealthPotion();

        return TaskStatus.COMPLETED;
    }

}
