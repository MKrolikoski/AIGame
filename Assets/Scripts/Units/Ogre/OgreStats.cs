using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreStats : UnitStats
{
    protected override void Awake()
    {
        base.Awake();
        moveRange = new Stat(3);
        attackRange = new Stat(2);
        attack = new Stat(35);
        defence = new Stat(0);
        hp = new HealthPoints(150, 5);
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