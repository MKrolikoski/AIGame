using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay_ButtonsUI : Overlay_Element
{

    SkillSlot[] skillSlots;
    ItemSlot[] itemSlots;
    Unit unit;
    
    void Start ()
    {
        skillSlots = GetComponentsInChildren<SkillSlot>();
        itemSlots = GetComponentsInChildren<ItemSlot>();
	}
	
	void Update ()
    {
        if (unit != null)
        {
            UpdateElement(unit);
        }
    }

    public override void UpdateElement(Unit selectedUnit)
    {
        base.UpdateElement(selectedUnit);
        UpdateSkills(selectedUnit);
        UpdateItems(selectedUnit);
        unit = selectedUnit;
    }

    private void UpdateSkills(Unit selectedUnit)
    {
        Skill[] skills = selectedUnit.skills.skills;
        if (unit != selectedUnit)
        {
            for (int i = 0; i < skillSlots.Length; i++)
            {
                skillSlots[i].ClearSkill();
            }
        }
        for (int i = 0; i < skills.Length; i++)
        {
            skillSlots[i].AddSkill(skills[i], selectedUnit);
        }
    }

    private void UpdateItems(Unit selectedUnit)
    {
        Item[] items = selectedUnit.inventory.items.ToArray();
        
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearItem();
        }
        for (int i = 0; i < items.Length; i++)
        {
            itemSlots[i].AddItem(items[i], selectedUnit);
            if(unit.unitOwner.GetType() != typeof(Player))
            {
                itemSlots[i].DeactivateButton();
            }
        }
    }
}
