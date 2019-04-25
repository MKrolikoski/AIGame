using UnityEngine;

public class UnitStats : MonoBehaviour {

    //Move range in tiles
    public Stat moveRange;
    //Attack range in tiles
    public Stat attackRange;
    //Attack value
    public Stat attack;
    //Defence value
    public Stat defence;
    //Leech value
    public Stat leech;
    //Health points
    public HealthPoints hp;
    //Mana points
    public ManaPoints mp;
    //Event triggered when getting hit
    public event System.Action OnHit;



    protected virtual void Awake()
    {
        moveRange = new Stat(3);
        attackRange = new Stat(1);
        attack = new Stat(10);
        defence = new Stat(0);
        leech = new Stat(0);
        hp = new HealthPoints(100);
        mp = new ManaPoints(0);
    }

    protected virtual void Start ()
    {
        hp.OnDeath += OnDeath;
        AddEventToStats();
    }

    protected virtual void AddEventToStats()
    {
        GameManager.instance.OnNewTurn += moveRange.OnNewTurn;
        GameManager.instance.OnNewTurn += attackRange.OnNewTurn;
        GameManager.instance.OnNewTurn += attack.OnNewTurn;
        GameManager.instance.OnNewTurn += defence.OnNewTurn;
        GameManager.instance.OnNewTurn += hp.OnNewTurn;
        GameManager.instance.OnNewTurn += hp.healthRegen.OnNewTurn;
        GameManager.instance.OnNewTurn += mp.OnNewTurn;
        GameManager.instance.OnNewTurn += mp.manaRegen.OnNewTurn;
        GameManager.instance.OnNewTurn += leech.OnNewTurn;

    }

    void Update ()
    {
		
	}

    protected virtual void OnDeath()
    {
        Unit unit = GetComponent<Unit>();
        unit.unitOwner.units.Remove(unit);
        GetComponent<Unit>().currentTile.ChangeSelectionToDefault();
    }

    public int TakeDamage(int damage)
    {
        int hpLost = hp.LoseHp(damage, defence.getValue());
        GetComponent<DamageNumberUI>().CreateDamageNumber(hpLost.ToString(), GetComponent<HealthUI>().target);

        if(OnHit != null && hp.getValue() > 0)
        {
            OnHit();
        }
        return hpLost;
    }
}
