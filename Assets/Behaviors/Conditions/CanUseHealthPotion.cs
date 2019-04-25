
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/CanUseHealthPotion")]
[Help("Check whether selected unit can use health potion")]
public class CanUseHealthPotion : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("CanUseHealthPotion: selectedUnit is null");
            return false;
        }
        return selectedUnit.inventory.HealthPotionInInventory();
    }
}