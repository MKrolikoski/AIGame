using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMinionStats : UnitStats
{
    protected override void Awake()
    {
        base.Awake();
        moveRange = new Stat(2);
        attackRange = new Stat(1);
        attack = new Stat(10);
        defence = new Stat(5);
        hp = new HealthPoints(25);
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