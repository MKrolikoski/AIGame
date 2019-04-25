using UnityEngine;

public class HealthPoints : Stat
{
    //Event fired when hp is reduced to or below 0
    public event System.Action OnDeath;
    //Event fired when current hp is changed
    public event System.Action<int, int> OnHealthChanged;

    public Stat healthRegen { get; private set; }


    public HealthPoints(int baseValue, int healthRegen = 0) : base(baseValue)
    {
        this.healthRegen = new Stat(healthRegen);
    }

    public int LoseHp(int damage, int defenceValue = 0)
    {
        damage -= defenceValue;
        damage = Mathf.Clamp(damage, 0, CurrentValue);
        CurrentValue -= damage;

        if(OnHealthChanged != null)
        {
            OnHealthChanged(baseValue, CurrentValue);
        }

        if(CurrentValue == 0)
        {
            if (OnDeath != null)
                OnDeath();
        }
        return damage;
    }

    public void GainHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, baseValue - CurrentValue);
        CurrentValue += hp;
        if (OnHealthChanged != null)
        {
            OnHealthChanged(baseValue, CurrentValue);
        }
    }

    public override void OnNewTurn()
    {
        base.OnNewTurn();
        if(healthRegen.getValue() > 0)
            GainHp(healthRegen.getValue());
    }
}
