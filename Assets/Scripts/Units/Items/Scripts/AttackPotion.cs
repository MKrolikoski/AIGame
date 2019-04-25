using UnityEngine;

[CreateAssetMenu(fileName = "Attack Potion", menuName = "Item/Attack Potion")]
public class AttackPotion : Item
{
    public int value;
    public int effectTime;

    public override void Use(Unit unit)
    {
        base.Use(unit);
        unit.stats.attack.AddModifier(new Modifier(value, effectTime));
    }
}
