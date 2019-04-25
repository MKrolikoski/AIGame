
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/CanUseDefensivePotion")]
[Help("Check whether selected unit can use defensive potion")]
public class CanUseDefensivePotion : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("CanUseDefensivePotion: selectedUnit is null");
            return false;
        }
        return selectedUnit.inventory.DefensivePotionInInventory();
    }
}