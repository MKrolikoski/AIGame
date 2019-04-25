
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/CanUseManaPotion")]
[Help("Check whether selected unit can use mana potion")]
public class CanUseManaPotion : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("CanUseManaPotion: selectedUnit is null");
            return false;
        }
        return selectedUnit.inventory.ManaPotionInInventory();
    }
}