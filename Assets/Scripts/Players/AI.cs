using cakeslice;
using System.Collections.Generic;
using UnityEngine;

public class AI : Commander
{

    public static AI instance;
    //public List<Unit> checkedUnits;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            return;
        instance = this;
        //checkedUnits = new List<Unit>();
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
            u.GetComponentInChildren<Outline>().color = 0;
            u.unitOwner = this;
            });
    }

    void Update ()
    {
		
	}

    public bool EnemyInUnitRangeAtTile(Unit unit, Tile tile, Unit enemy)
    {
        Vector3 tilePosition = tile.transform.position;
        tilePosition.y += 1;
        Vector3 direction = -(tilePosition - enemy.transform.position).normalized;
        for (int i = -15; i < 15; i++)
        {
            Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
            Ray ray = new Ray(tilePosition, q * direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4 * unit.stats.attackRange.getValue(), attackMask))
            {
                Unit hitUnit = hit.collider.GetComponent<Unit>();
                if (hitUnit != null && hitUnit == enemy)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override void StartTurn()
    {
        base.StartTurn();
        checkedUnits = new List<Unit>();
    }
}
