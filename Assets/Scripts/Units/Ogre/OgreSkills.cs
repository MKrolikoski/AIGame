using UnityEngine;

public class OgreSkills : UnitSkills
{
    public Berzerk berzerkScriptableObject;
    public ThickSkin thickSkinScriptableObject;



    protected override void Awake()
    {
        base.Awake();
        skills = new Skill[2];
    }

    protected override void Start()
    {
        base.Start();
        Skill berzerk = Instantiate(berzerkScriptableObject);
        skills[0] = berzerk;
        Skill thickSkin = Instantiate(thickSkinScriptableObject);
        skills[1] = thickSkin;
    }

    protected override void Update()
    {

    }



    public override bool UseDefensiveSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(ThickSkin))
                return skills[i].Use(GetComponent<Unit>());
        }
        return false;
    }

    public override bool UseOffensiveSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(Berzerk))
                return skills[i].Use(GetComponent<Unit>());
        }
        return false;
    }

    public override bool CanUseOffensiveSkill()
    {
        Skill skill = null;
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(Berzerk))
            {
                skill = skills[i];
                break;
            }
        }
        if (skill != null && skill.isActive && GetComponent<UnitStats>().mp.getValue() >= skill.manaCost)
            return true;
        return false;
    }

    public override bool CanUseDefensiveSkill()
    {
        Skill skill = null;
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(ThickSkin))
            {
                skill = skills[i];
                break;
            }
        }
        if (skill != null && skill.isActive && GetComponent<UnitStats>().mp.getValue() >= skill.manaCost)
            return true;
        return false;
    }
}
