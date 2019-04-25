using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMotor))]
[RequireComponent(typeof(UnitStats))]
[RequireComponent(typeof(UnitCombat))]
public class Unit : Selectable
{
    //Layer with tiles
    public LayerMask movementMask;
    //Selection ring
    public Transform selection;
    //Units stats;
    public UnitStats stats;
    //Unit skills
    public UnitSkills skills;
    //Unit inventory
    public UnitInventory inventory;
    //Unit combat script
    public UnitCombat unitCombat;
    //Queued tiles to move through
    private List<Tile> pathToMove;
    //Tile on which the unit is located
    public Tile currentTile;
    //Delegate fired when moved through tiles in pathToMove
    public delegate void MovedThroughTiles();
    public MovedThroughTiles onMovedThroughTiles;
    //Player to whom the unit belongs
    public Commander unitOwner;
    //Name of the uni
    public string unitName = "Unit";
    //Is killing this unit required for opponent to win the game
    public bool isEssential = false;

    protected virtual void Awake()
    {
        pathToMove = new List<Tile>();
    }

    protected virtual void Start()
    {
        GetComponent<UnitMotor>().onMoveFinished += MoveToNextTile;
    }


    private void LateUpdate()
    {
        if (currentTile == null)
        {
            currentTile = getCurrentTile();
        }
    }

    public Tile getCurrentTile()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, movementMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                tile.PutUnit(this);
                return tile;
            }
        }
        return null;
    }


    public void FaceDirection(Vector3 direction)
    {
        Vector3 d = (direction - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(d.x, 0, d.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);
    }


    public void HighlightUnit()
    {
        if (selection != null)
        {
            selection.GetComponent<Selection>().Show();
        }
    }

    public void DehighlightUnit()
    {
        if (selection != null)
        {
            selection.GetComponent<Selection>().Hide();
        }
    }

    public override void OnSelect()
    {
        base.OnSelect();
        ShowHp();
        GetComponent<HealthUI>().active = true;
        selection.GetComponent<Selection>().Show();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        //HideHp();
        GetComponent<HealthUI>().active = false;
        selection.GetComponent<Selection>().Hide();
    }

    public void ShowHp()
    {
        GetComponent<HealthUI>().activateHealthBar();
    }

    public void HideHp()
    {
        GetComponent<HealthUI>().deactivateHealthBar();
    }

    public void MoveToPoint(Vector3 point)
    {
        GetComponent<UnitMotor>().MoveToPoint(point);
    }

    public void MoveThroughTiles(List<Tile> tiles)
    {
        pathToMove = tiles;
        MoveToNextTile();
    }

    private void MoveToNextTile()
    {
        currentTile.UnitMovedThroughTile();
        if (pathToMove.Count != 0)
        {
            pathToMove[0].PutUnit(this);
            currentTile = pathToMove[0];
            pathToMove.RemoveAt(0);
        }
        else
        {
            currentTile.unitOnTile = this;
            currentTile.walkable = false;
            if (onMovedThroughTiles != null)
            {
                onMovedThroughTiles.Invoke();
            }
        }
    }

    public void Death_AnimationEvent()
    {
        currentTile.UnitDied();
        if(isEssential)
        {
            GameManager.instance.EssentialUnitDied(unitOwner);
        }
        Destroy(gameObject);
    }

    public void Attack(Unit target, bool rangedAttack = false)
    {
        unitCombat.Attack(target, rangedAttack);
    }


}
