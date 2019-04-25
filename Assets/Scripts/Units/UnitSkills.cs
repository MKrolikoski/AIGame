using UnityEngine;

public class UnitSkills : MonoBehaviour
{
    public Skill[] skills;


    protected virtual void Awake()
    {
    }

    protected virtual void Start ()
    {
		
	}

    protected virtual void Update ()
    {
		
	}

    public virtual bool CanUseOffensiveSkill()
    {
        return false;
    }

    public virtual bool CanUseDefensiveSkill()
    {
        return false;
    }

    public virtual bool UseOffensiveSkill()
    {
        return false;
    }

    public virtual bool UseDefensiveSkill()
    {
        return false;
    }

}
