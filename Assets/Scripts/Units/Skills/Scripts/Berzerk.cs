using UnityEngine;

[CreateAssetMenu(fileName = "Berzerk", menuName = "Skill/Berzerk")]
public class Berzerk : Skill
{
    public int newAttackValue;
    public int newAttackDuration;
    public int newAttackRangeValue;
    public int newAttackRangeDuration;
    public int newMoveRangeValue;
    public int newMoveRangeDuration;
    public int hpCost;


    public override bool Use(Unit unit)
    {
        if (!base.Use(unit))
            return false;

        Modifier attackMod = new Modifier(newAttackValue, newAttackDuration);
        Modifier attakRangeMod = new Modifier(newAttackRangeValue, newAttackRangeDuration);
        Modifier moveRangeMod = new Modifier(newMoveRangeValue, newMoveRangeDuration);

        unit.stats.attack.AddModifier(attackMod);
        unit.stats.attackRange.AddModifier(attakRangeMod);
        unit.stats.moveRange.AddModifier(moveRangeMod);
        int unitHp = unit.stats.hp.getValue();
        unit.stats.hp.LoseHp(Mathf.Clamp(hpCost, 0, unitHp-1));

        return true;
    }
}
