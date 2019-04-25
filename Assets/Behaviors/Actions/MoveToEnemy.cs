using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;
using System.Collections.Generic;

[Action("Custom/Actions/MoveToEnemy")]
[Help("Moves selected unit to the specified enemy")]
public class MoveToEnemy : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    [InParam("Enemy")]
    public Unit enemy;

    private bool finishedMoving = false;


    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("MoveToEnemy: selectedUnit is null");
            return TaskStatus.FAILED;
        }
        if (enemy == null)
        {
            Debug.Log("MoveToEnemy: enemy is null");
            return TaskStatus.FAILED;
        }
        if (!selectedUnit.GetComponent<UnitMotor>().isMoving && !finishedMoving)
        {
            List<Tile> pathToMove = PathManager.instance.FindPath(selectedUnit.currentTile, enemy.currentTile, selectedUnit.stats.moveRange.getValue());
            pathToMove = CheckShorterPath(pathToMove);
            if (pathToMove.Count == 0)
            {
                GameManager.instance.unitMoved = true;
                return TaskStatus.COMPLETED;
            }
            GameManager.instance.SelectedTiles = pathToMove;
            selectedUnit.onMovedThroughTiles += OnUnitFinishedMoving;
            GameManager.instance.MoveUnit();

        }
        if (finishedMoving)
        {
            //Debug.Log("MoveToEnemy: finishedMoving is true");
            return TaskStatus.COMPLETED;
        }
        else
            return TaskStatus.RUNNING;
    }

    private void OnUnitFinishedMoving()
    {
        finishedMoving = true;
        selectedUnit.onMovedThroughTiles -= OnUnitFinishedMoving;
    }

    private List<Tile> CheckShorterPath(List<Tile> pathToMove)
    {
        //if (selectedUnit.unitCombat.EnemyInAttackRange(enemy, AI.instance.attackMask))
        if (selectedUnit.unitCombat.EnemyInAttackRange(enemy, selectedUnit.unitOwner.attackMask))
            return new List<Tile>();
        for (int i = 0; i < pathToMove.Count; i++)
        {
            //if (selectedUnit.unitCombat.EnemyInAttackRangeFromTile(enemy, pathToMove[i], AI.instance.attackMask))
            if (selectedUnit.unitCombat.EnemyInAttackRangeFromTile(enemy, pathToMove[i], selectedUnit.unitOwner.attackMask))
            {
                return pathToMove.GetRange(0, i+1);
            }
        }

        return pathToMove;
    }
}
