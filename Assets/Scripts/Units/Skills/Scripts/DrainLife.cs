using UnityEngine;

[CreateAssetMenu(fileName = "Drain Life", menuName = "Skill/Drain Life")]
public class DrainLife : Skill
{
    public int newLeechValue;
    public int newLeechDuration;
    public int newAttackValue;
    public int newAttackDuration;


    public override bool Use(Unit unit)
    {
        if (!base.Use(unit))
            return false;

        Modifier leechMod = new Modifier(newLeechValue, newLeechDuration);
        unit.stats.leech.AddModifier(leechMod);
        Modifier attackMod = new Modifier(newAttackValue, newAttackDuration);
        unit.stats.attack.AddModifier(attackMod);
        return true;
    }
}
