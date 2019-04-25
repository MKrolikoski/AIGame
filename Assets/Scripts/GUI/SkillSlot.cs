using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour {

    public Skill skill { get; private set; }
    public Unit unit { get; private set; }
    public Image icon;
    public Button skillButton;
    public Text cooldownText;

    private void Update()
    {
        if (skill != null && skill.isActivable && skill.isActive && unit.unitOwner.GetType() == typeof(Player))
        {
            skillButton.enabled = true;
            ChangeIconAlpha(1);
        }
        else if (skillButton.enabled)
        {
            ChangeIconAlpha(0.4f);
            skillButton.enabled = false;
        }
        if(skill != null && skill.leftCooldown > 0)
        {
            cooldownText.enabled = true;
            cooldownText.text = skill.leftCooldown.ToString();
        }
        else
        {
            cooldownText.enabled = false;
        }
    }

    public void AddSkill(Skill skill, Unit unit)
    {
        this.skill = skill;
        this.unit = unit;

        icon.sprite = skill.icon;
        icon.enabled = true;
    }

    public void ClearSkill()
    {
        skill = null;
        unit = null;
        icon.sprite = null;
        icon.enabled = false;
        cooldownText.text = null;
        cooldownText.enabled = false;
    }

    public void UseSkill()
    {
        if(skill != null && unit != null)
        {
            if (skill.Use(unit))
            {
                //Everything is OK
            }
            else
            {
                //Not enough mana
            }
        }
    }

    public void ChangeIconAlpha(float value)
    {
        value = Mathf.Clamp(value,0,1);
        Color color = icon.color;
        color.a = value;
        icon.color = color;
    }
}
