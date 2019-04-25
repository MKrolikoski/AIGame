using UnityEngine;

public class Skill : ScriptableObject
{
    new public string name = "New Skill";
    public Sprite icon;
    public bool isActivable = true;
    public int cooldown;
    public int leftCooldown;
    public int manaCost;
    public bool isActive = true;


    public virtual bool Use(Unit unit)
    {
        if (unit.stats.mp.getValue() < manaCost)
            return false;
        unit.stats.mp.LoseMana(manaCost);
        leftCooldown = cooldown;
        isActive = false;
        GameManager.instance.OnNewTurn += OnNewTurn;
        return true;
    }

    public void ReduceCooldown(int reductionValue = 1)
    {
        leftCooldown -= reductionValue;
        if(leftCooldown == 0)
        {
            isActive = true;
            GameManager.instance.OnNewTurn -= OnNewTurn;
        }
    }

    protected void OnNewTurn()
    {
        ReduceCooldown();
    }
}
