using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    private UnitStats stats;
    private UnitStats enemyStats;
    [HideInInspector]
    public bool rangedAttack;
    public event System.Action OnAttack;
    public event System.Action FinishedAttacking;


    void Start ()
    {
        stats = GetComponent<UnitStats>();
	}


    void Update ()
    {
		
	}

    public void Attack(Unit target, bool rangedAttack)
    {
        enemyStats = target.stats;
        target.ShowHp();
        this.rangedAttack = rangedAttack;
        if (OnAttack != null)
        {
            OnAttack();
        }
    }

    public void AttackHit_AnimationEvent()
    {
        int attackValueOffset = Mathf.RoundToInt(stats.attack.getValue() * 0.2f);
        int attackValue = stats.attack.getValue();
        int damage = Mathf.RoundToInt(Random.Range(attackValue-attackValueOffset, attackValue + attackValueOffset));
        int hpLost = enemyStats.TakeDamage(damage);
        if(stats.leech.getValue() != 0)
        {
            int hpGain = Mathf.RoundToInt(hpLost * (stats.leech.getValue() / 100f));
            stats.hp.GainHp(hpGain);
        }
        if(enemyStats.hp.getValue() <= 0)
        {
            OnUnitKilled();
        }
        if (FinishedAttacking != null)
        {
            FinishedAttacking();
        }
    }

    protected void OnUnitKilled()
    {
        UnitInventory inv = GetComponent<UnitInventory>();
        if(inv.SpaceInInventory())
        {
            Item dropedItem = ItemSpawner.instance.SpawnItem(stats);
            if (dropedItem != null)
            {
                inv.AddItem(dropedItem);
            }
        }
    }

    public bool EnemyInAttackRange(Unit enemy, LayerMask attackRangeMask)
    {
        Vector3 unitPosition = transform.position;
        unitPosition.y += 1;
        Vector3 direction = -(transform.position - enemy.transform.position).normalized;
        for (int i = -15; i < 15; i++)
        {
            Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
            Ray ray = new Ray(unitPosition, q * direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4 * stats.attackRange.getValue(), attackRangeMask))
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if(unit != null && unit == enemy)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool EnemyInAttackRangeFromTile(Unit enemy, Tile tile, LayerMask attackRangeMask)
    {
        Vector3 attackPosition = tile.transform.position;
        attackPosition.y += 1;
        Vector3 direction = -(transform.position - enemy.transform.position).normalized;
        for (int i = -15; i < 15; i++)
        {
            Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
            Ray ray = new Ray(attackPosition, q * direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4 * stats.attackRange.getValue(), attackRangeMask))
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null && unit == enemy)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public List<Tile> TilesWithEnemiesInRange(LayerMask attackRangeMask)
    {
        List<Tile> tilesWithEnemies = new List<Tile>();
        for (int i = 0; i < 360; i++)
        {
            Vector3 unitPosition = transform.position;
            unitPosition.y += 1;
            Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
            Ray ray = new Ray(unitPosition, q * Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4 * stats.attackRange.getValue(), attackRangeMask))
            {
                Unit hitUnit = hit.collider.GetComponent<Unit>();
                if (hitUnit != null)
                {
                    if (!tilesWithEnemies.Contains(hitUnit.currentTile) && hitUnit.unitOwner != GetComponent<Unit>().unitOwner)
                        tilesWithEnemies.Add(hitUnit.currentTile);
                }
            }
        }
        return tilesWithEnemies;
    }

    public List<Unit> EnemiesInAttackRange(LayerMask attackRangeMask)
    {
        List<Unit> enemies = new List<Unit>();
        for (int i = 0; i < 360; i++)
        {
            Vector3 unitPosition = transform.position;
            unitPosition.y += 1;
            Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
            Ray ray = new Ray(unitPosition, q * Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4 * stats.attackRange.getValue(), attackRangeMask))
            {
                Unit hitUnit = hit.collider.GetComponent<Unit>();
                if (hitUnit != null)
                {
                    if (!enemies.Contains(hitUnit))
                        enemies.Add(hitUnit);
                }
            }
        }
        return enemies;
    }

    public int DistanceToEnemy(Unit enemy)
    {
        int distance = Mathf.Abs(Mathf.RoundToInt((transform.position.x - enemy.transform.position.x) / 4)) + Mathf.Abs(Mathf.RoundToInt((transform.position.z - enemy.transform.position.z) / 4));
        return distance;
    }
}
