
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using System.Collections.Generic;
using UnityEngine;

[Condition("Custom/Conditions/EssentialUnitInRange")]
[Help("Check whether there is essential unit at specified distance")]
public class EssentialUnitInRange : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    [InParam("PrioritizeEssential", DefaultValue = true)]
    public bool prioritizeEssential;

    [InParam("EssentialUnitMaxDistance", DefaultValue = 5)]
    public int essentialUnitMaxDistance;

    public override bool Check()
    {
        if (selectedUnit == null)
        {
            Debug.Log("EssentialUnitInRange: selectedUnit is null");
            return false;
        }
        return prioritizeEssential ? IsEssentialUnitInRange() : false;
    }

    private bool IsEssentialUnitInRange()
    {
        Unit[] enemies = Player.instance.units.ToArray();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].isEssential && selectedUnit.unitCombat.DistanceToEnemy(enemies[i]) <= essentialUnitMaxDistance)
            {
                return true;
            }
        }
        return false;
    }
}