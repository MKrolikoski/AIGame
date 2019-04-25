using UnityEngine;

[CreateAssetMenu(fileName = "Wraith Form", menuName = "Skill/Wraith Form")]
public class WraithForm : Skill
{
    public int newAttackValue;
    public int newAttackDuration;
    public int newDefenceValue;
    public int newDefencekDuration;
    public int newMoveRangeValue;
    public int newMoveRangeDuration;



    public override bool Use(Unit unit)
    {
        if (!base.Use(unit))
            return false;

        Modifier attackMod = new Modifier(newAttackValue, newAttackDuration);
        Modifier defenceMod = new Modifier(newDefenceValue, newDefencekDuration);
        Modifier moveRangeMod = new Modifier(newMoveRangeValue, newMoveRangeDuration);
        unit.stats.attack.AddModifier(attackMod);
        unit.stats.defence.AddModifier(defenceMod);
        unit.stats.moveRange.AddModifier(moveRangeMod);
        return true;
    }
}
