using UnityEngine;

[RequireComponent(typeof(Selection))]
public class Tile : Selectable
{
    //Tile to the right
    [HideInInspector]
    public Tile rightNeighbourTile;
    //Tile to the left
    [HideInInspector]
    public Tile leftNeighbourTile;
    //Upper tile
    [HideInInspector]
    public Tile upperNeighbourTile;
    //Lower tile
    [HideInInspector]
    public Tile lowerNeighbourTile;
    //Layer with tiles
    public LayerMask tileMask;
    //Selection ring
    public Transform selection;
    //Indicates if unit can walk on this tile
    public bool walkable;
    //Current unit on the tile
    public Unit unitOnTile;



    #region A* values
    //A* values
    public int gCost, hCost;
    public int fCost { get { return gCost + hCost; } }
    public Tile parent;
    #endregion


    void Start ()
    {
        SetNeighbouringTiles();
    }



    public void HighlightTile()
    {
        if(selection != null)
        {
            selection.GetComponent<Selection>().Show();
        }
    }

    public void DehighlightTile()
    {
        if(selection != null)
        {
            selection.GetComponent<Selection>().Hide();
        }
    }


    public override void OnSelect()
    {
        base.OnSelect();
        HighlightTile();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        DehighlightTile();
    }

    #region neighbours_init
    private void SetNeighbouringTiles()
    {
        SetRightNeighbouringTile();
        SetLeftNeighbouringTile();
        SetUpperNeighbouringTile();
        SetLowerNeighbouringTile();
    }

    private void SetRightNeighbouringTile()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.right);
        if (Physics.Raycast(ray, out hit, 100, tileMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                rightNeighbourTile = tile;
            }
            else
            {
                rightNeighbourTile = null;
            }
        }
    }

    private void SetLeftNeighbouringTile()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.right);
        if (Physics.Raycast(ray, out hit, 100, tileMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                leftNeighbourTile = tile;
            }
            else
            {
                leftNeighbourTile = null;
            }
        }
    }

    private void SetUpperNeighbouringTile()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100, tileMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                upperNeighbourTile = tile;
            }
            else
            {
                upperNeighbourTile = null;
            }
        }
    }

    private void SetLowerNeighbouringTile()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.forward);
        if (Physics.Raycast(ray, out hit, 100, tileMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                lowerNeighbourTile = tile;
            }
            else
            {
                lowerNeighbourTile = null;
            }
        }
    }
    #endregion

    public void UnitMovedThroughTile()
    {
        DehighlightTile();
        unitOnTile = null;
        walkable = true;
    }

    public void PutUnit(Unit unit)
    {
        unitOnTile = unit;
        unit.MoveToPoint(transform.position);
        walkable = false;
    }

    public void UnitDied()
    {
        unitOnTile = null;
        walkable = true;
    }

    public void ChangeSelectionToDefault()
    {
        selection.GetComponent<Selection>().ChangeSelectionToDefault();
    }
    public void ChangeSelectionToCloseRange()
    {
        selection.GetComponent<Selection>().ChangeSelectionToCloseRange();
    }
    public void ChangeSelectionToRange()
    {
        selection.GetComponent<Selection>().ChangeSelectionToRange();
    }

    public bool IsAdjacentTile(Tile tile)
    {
        if (Mathf.Round(Mathf.Abs(transform.position.x - tile.transform.position.x)) <= 4 && Mathf.Round(Mathf.Abs(transform.position.z - tile.transform.position.z)) <= 4)
        {
            return true;
        }
        return false;
    }
}
