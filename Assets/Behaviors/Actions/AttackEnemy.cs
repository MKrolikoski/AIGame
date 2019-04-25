using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

[Action("Custom/Actions/AttackEnemy")]
[Help("Attacks enemy")]
public class AttackEnemy : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    [InParam("Enemy")]
    public Unit enemy;

    private bool finishedAttacking = false;
    private bool attacking = false;


    public override TaskStatus OnUpdate()
    {
        if (selectedUnit == null)
        {
            Debug.Log("AttackEnemy: selectedUnit is null");
            return TaskStatus.FAILED;
        }
        if (enemy == null)
        {
            Debug.Log("AttackEnemy: enemy is null");
            return TaskStatus.FAILED;
        }
        if(!attacking)
        {
            GameManager.instance.AttackUnit();
            selectedUnit.unitCombat.FinishedAttacking += SelectedUnitFinishedAttacking;
            attacking = true;
        }
        if(!finishedAttacking)
        {
            return TaskStatus.RUNNING;
        }
        return TaskStatus.COMPLETED;
    }

    private void SelectedUnitFinishedAttacking()
    {
        finishedAttacking = true;
        selectedUnit.unitCombat.FinishedAttacking -= SelectedUnitFinishedAttacking;
    }
}
