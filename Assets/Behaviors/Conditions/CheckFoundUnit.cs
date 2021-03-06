﻿
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using System.Collections.Generic;
using UnityEngine;

[Condition("Custom/Conditions/CheckFoundUnit")]
[Help("Check found unit if it's essential, already checked or there are enemies nearby")]
public class CheckFoundUnit : ConditionBase
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit.isEssential || AI.instance.checkedUnits.Contains(selectedUnit))
        {
            return true;
        }
        List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(AI.instance.attackMask);
        if (enemies.Count == 0)
        {
            AI.instance.checkedUnits.Add(selectedUnit);
            return false;
        }
        return true;
    }
}