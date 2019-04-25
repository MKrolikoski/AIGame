using UnityEngine;

[CreateAssetMenu(fileName = "Defence Potion", menuName = "Item/Defence Potion")]
public class DefencePotion : Item
{
    public int value;
    public int effectTime;

    public override void Use(Unit unit)
    {
        base.Use(unit);
        unit.stats.defence.AddModifier(new Modifier(value, effectTime));
    }
}
