using cakeslice;
using UnityEngine;

public class Player : Commander
{
    public static Player instance;
    public int outlineColor;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    protected override void Start()
    {
        base.Start();

        InitUnits();
    }

    protected override void InitUnits()
    {
        base.InitUnits();
        units.ForEach(u => {
            u.GetComponentInChildren<Outline>().color = outlineColor;
            u.unitOwner = this;
        });
    }

    void Update ()
    {
		
	}
}
