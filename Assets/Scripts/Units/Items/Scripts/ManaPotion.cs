using UnityEngine;

[CreateAssetMenu(fileName = "Mana Potion", menuName = "Item/Mana Potion")]
public class ManaPotion : Item
{
    public int value;

    public override void Use(Unit unit)
    {
        base.Use(unit);
        unit.stats.mp.GainMana(value);
    }
}
