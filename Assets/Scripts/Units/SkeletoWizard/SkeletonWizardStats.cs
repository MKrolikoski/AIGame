using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWizardStats : UnitStats
{

    protected override void Awake()
    {
        base.Awake();
        moveRange = new Stat(6);
        attackRange = new Stat(5);
        attack = new Stat(25);
        defence = new Stat(0);
        hp = new HealthPoints(100);
        mp = new ManaPoints(100);
    }


    protected override void Start()
    {
        base.Start();

    }

    void Update()
    {

    }



    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponentInChildren<Animator>().Play("Death");
    }
}
