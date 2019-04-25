using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {

    //Number of moves available at the start of the turn
    public int availableMovesInTurn;
    //Number of moves left in current turn
    public int availableMovesLeft;
    public int essentialUnits;
    public LayerMask attackMask;
    public List<Unit> units;

    public List<Unit> checkedUnits;


    protected virtual void Awake()
    {
        availableMovesInTurn = 3;
        units = new List<Unit>(GetComponentsInChildren<Unit>());
        checkedUnits = new List<Unit>();
    }
    protected virtual void Start ()
    {
	}

	
	protected virtual void InitUnits()
    {
        units.ForEach(u => {
            if (u.isEssential)
                essentialUnits++;
        });
    }

    public virtual void UnitEndedMove(Unit unit)
    {
        unit.canBeSelected = false;
        availableMovesLeft--;
    }

    public virtual void StartTurn()
    {
        units.ForEach(u => u.canBeSelected = true);
        if (units.Count < availableMovesInTurn)
            availableMovesInTurn = units.Count;
        availableMovesLeft = availableMovesInTurn;
    }

    public virtual void EndTurn()
    {
        units.ForEach(u => u.canBeSelected = false);
    }
}
