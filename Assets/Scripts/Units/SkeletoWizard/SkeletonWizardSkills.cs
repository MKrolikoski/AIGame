using UnityEngine;

public class SkeletonWizardSkills : UnitSkills
{
    public WraithForm wraithFormScriptableObject;
    public DrainLife drainLifeScriptableObject;



    protected override void Awake()
    {
        base.Awake();
        skills = new Skill[2];
    }

    protected override void Start()
    {
        base.Start();
        Skill wraithForm = Instantiate(wraithFormScriptableObject);
        skills[0] = wraithForm;
        Skill drainLife = Instantiate(drainLifeScriptableObject);
        skills[1] = drainLife;

    }

    protected override void Update()
    {

    }

    public override bool UseDefensiveSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(WraithForm))
                return skills[i].Use(GetComponent<Unit>());
        }
        return false;
    }

    public override bool UseOffensiveSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(DrainLife))
                return skills[i].Use(GetComponent<Unit>());
        }
        return false;
    }

    public override bool CanUseOffensiveSkill()
    {
        Skill skill = null;
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetType() == typeof(DrainLife))
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
            if (skills[i].GetType() == typeof(WraithForm))
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
