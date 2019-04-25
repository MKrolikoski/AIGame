using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Item/Health Potion")]
public class HealthPotion : Item
{
    public int value;

    public override void Use(Unit unit)
    {
        base.Use(unit);
        unit.stats.hp.GainHp(value);
    }
}
