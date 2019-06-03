using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;
using System.Collections.Generic;

[Action("Custom/Actions/FindEnemyAtDistance")]
[Help("Finds enemy closer or at specified distance")]
public class FindEnemyAtDistance : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    [InParam("PrioritizeEssential", DefaultValue = true)]
    public bool prioritizeEssential;

    [InParam("EssentialUnitMaxDistance", DefaultValue = 5)]
    public int essentialUnitMaxDistance;

    [OutParam("Enemy")]
    public Unit enemy;


    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("FindEnemyAtDistance: selectedUnit is null");
            return TaskStatus.FAILED;
        }
        enemy = prioritizeEssential ? FindEssentialEnemy() : FindClosestEnemy();
        return TaskStatus.COMPLETED;
    }

    private Unit FindClosestEnemy()
    {
        List<Unit> enemies = Player.instance.units;
        int enemyDistance = selectedUnit.unitCombat.DistanceToEnemy(enemies[0]);
        Unit closestEnemy = enemies[0];
        foreach (Unit enemy in enemies)
        {
            int tmpDistance = selectedUnit.unitCombat.DistanceToEnemy(enemy);
            if (tmpDistance < enemyDistance)
            {
                closestEnemy = enemy;
                enemyDistance = tmpDistance;
            }
        }
        return closestEnemy;
    }

    private Unit FindEssentialEnemy()
    {
        List<Unit> enemies = Player.instance.units;
        int enemyDistance = 0;
        int lastEnemyDistance = 0;
        Unit closestEnemy = null;
        foreach (Unit enemy in enemies)
        {
            if (enemy.isEssential)
            {
                enemyDistance = selectedUnit.unitCombat.DistanceToEnemy(enemy);
                if (lastEnemyDistance != 0)
                {
                    if (enemyDistance < lastEnemyDistance)
                    {
                        closestEnemy = enemy;
                        lastEnemyDistance = enemyDistance;
                    }
                }
                else
                {
                    if (enemyDistance < essentialUnitMaxDistance)
                    {
                        closestEnemy = enemy;
                        lastEnemyDistance = enemyDistance;
                    }
                }
            }
        }
        if (closestEnemy == null)
            closestEnemy = FindClosestEnemy();
        return closestEnemy;
    }
}
