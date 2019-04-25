using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorStats : UnitStats
{
    protected override void Awake()
    {
        base.Awake();
        moveRange = new Stat(4);
        attackRange = new Stat(1);
        attack = new Stat(20);
        defence = new Stat(1);
        hp = new HealthPoints(100);
    }


    protected override void Start()
    {
        base.Start();
	}
	
	void Update ()
    {
		
	}

    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponentInChildren<Animator>().Play("Death");
    }
}
