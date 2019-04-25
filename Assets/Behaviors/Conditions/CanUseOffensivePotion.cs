
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/CanUseOffensivePotion")]
[Help("Check whether selected unit can use offensive potion")]
public class CanUseOffensivePotion : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("CanUseOffensivePotion: selectedUnit is null");
            return false;
        }
        return selectedUnit.inventory.OffensivePotionInInventory();
    }
}