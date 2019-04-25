
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using System.Collections.Generic;
using UnityEngine;

[Condition("Custom/Conditions/CheckEnemiesInRangeOfUnit")]
[Help("Check whether there are any enemies in unit's range")]
public class CheckEnemiesInRangeOfUnit : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        //List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(AI.instance.attackMask);
        List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(selectedUnit.unitOwner.attackMask);

        if (enemies.Count == 0)
        {
            //AI.instance.checkedUnits.Add(selectedUnit);
            selectedUnit.unitOwner.checkedUnits.Add(selectedUnit);

            return false;
        }
        return true;
    }
}