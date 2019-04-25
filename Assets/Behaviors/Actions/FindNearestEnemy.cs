using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;
using System.Collections.Generic;

[Action("Custom/Actions/FindNearestEnemy")]
[Help("Finds nearest enemy")]
public class FindNearestEnemy : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    [OutParam("Enemy")]
    public Unit enemy;


    [InParam("essentialPriority", DefaultValue = 5)]
    public int essentialPriority;
    [InParam("lowhealthPriority", DefaultValue = 3)]
    public int lowhealthPriority;
    [InParam("nearestPriority", DefaultValue = 0)]
    public int nearestPriority;



    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("FindNearestEnemy: selectedUnit is null");
            return TaskStatus.FAILED;
        }
        enemy = FindEnemy();
        GameManager.instance.SelectEnemyTile(enemy.currentTile);
        return TaskStatus.COMPLETED;
    }

    private Unit FindClosestEnemy()
    {
        List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(selectedUnit.unitOwner.attackMask);
        //List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(AI.instance.attackMask);
        if(enemies.Count == 0)
        {
            Debug.Log("FindNearestEnemy: enemies is empty");
        }
        int distance = selectedUnit.unitCombat.DistanceToEnemy(enemies[0]);
        Unit closestEnemy = enemies[0];
        foreach (Unit enemy in enemies)
        {
            int tmpDistance = selectedUnit.unitCombat.DistanceToEnemy(enemy);
            if(tmpDistance < distance)
            {
                distance = tmpDistance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    private Unit FindEssentialUnit()
    {
        List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(selectedUnit.unitOwner.attackMask);
        //List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(AI.instance.attackMask);
        if (enemies.Count == 0)
        {
            Debug.Log("FindNearestEnemy: enemies is empty");
        }
        Unit essentialEnemy = null;
        foreach (Unit enemy in enemies)
        {
            if(enemy.isEssential && enemy.stats.defence.getValue() < selectedUnit.stats.attack.getValue())
            {
                essentialEnemy = enemy;
            }
        }
        return essentialEnemy;
    }

    private Unit FindLowestHealthUnit()
    {
        List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(selectedUnit.unitOwner.attackMask);
        //List<Unit> enemies = selectedUnit.unitCombat.EnemiesInAttackRange(AI.instance.attackMask);
        if (enemies.Count == 0)
        {
            Debug.Log("FindNearestEnemy: enemies is empty");
        }
        Unit lowestHpEnemy = enemies[0];
        int lowestHp = enemies[0].stats.hp.getValue();
        foreach (Unit enemy in enemies)
        {
            if (enemy.stats.hp.getValue() < lowestHp && enemy.stats.defence.getValue() < selectedUnit.stats.attack.getValue())
            {
                lowestHpEnemy = enemy;
                lowestHp = enemy.stats.hp.getValue();
            }
        }
        return lowestHpEnemy;
    }

    private Func<Unit> PopHighestPriorityFunc(Dictionary<int, Func<Unit>> dic)
    {
        int maxPriority = -1;
        Func<Unit> func = FindClosestEnemy;
        foreach (var pair in dic)
        {
            if (pair.Key > maxPriority)
            {
                func = pair.Value;
                maxPriority = pair.Key;
            }
        }
        dic.Remove(maxPriority);
        return func;
    }

    private Unit FindEnemy()
    {
        Dictionary<int, Func<Unit>> priorityDic = new Dictionary<int, Func<Unit>>()
        {
            {essentialPriority, FindEssentialUnit},
            {lowhealthPriority, FindLowestHealthUnit},
            {nearestPriority, FindClosestEnemy}
        };


        Unit foundEnemy = null;
        while(foundEnemy == null)
        {
            Func<Unit> func = PopHighestPriorityFunc(priorityDic);
            foundEnemy = func.Invoke();
        }
        return foundEnemy;
    }
}
