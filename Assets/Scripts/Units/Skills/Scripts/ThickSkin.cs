using UnityEngine;

[CreateAssetMenu(fileName = "Thick Skin", menuName = "Skill/Thick Skin")]
public class ThickSkin : Skill
{
    public int newAttackValue;
    public int newAttackDuration;
    public int newDefenceValue;
    public int newDefenceDuration;
    public int newHealthRegenValue;
    public int newHealthRegenDuration;


    public override bool Use(Unit unit)
    {
        if (!base.Use(unit))
            return false;

        Modifier attackMod = new Modifier(newAttackValue, newAttackDuration);
        Modifier defenceMod = new Modifier(newDefenceValue, newDefenceDuration);
        Modifier healthRegenMod = new Modifier(newHealthRegenValue, newHealthRegenDuration);
        unit.stats.attack.AddModifier(attackMod);
        unit.stats.defence.AddModifier(defenceMod);
        unit.stats.hp.healthRegen.AddModifier(healthRegenMod);
        return true;
    }
}
