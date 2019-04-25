using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    //PathManager singleton
    public static PathManager instance;
    //Current path to specified tile
    private List<Tile> currentPath;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        currentPath = new List<Tile>();
    }



    public List<Tile> FindPath(Tile startingTile, Tile selectedTile, int unitRange)
    {
        List<Tile> openList = new List<Tile>();
        HashSet<Tile> closedList = new HashSet<Tile>();

        openList.Add(startingTile);

        while(openList.Count > 0)
        {
            Tile currentTile = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost < currentTile.fCost || openList[i].fCost == currentTile.fCost && openList[i].hCost < currentTile.hCost)
                {
                    currentTile = openList[i];
                }
            }
            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if(currentTile == selectedTile)
            {
                GetFinalPath(startingTile, selectedTile, unitRange);
                return currentPath;
            }

            List<Tile> neighbouringTiles = new List<Tile>();
            neighbouringTiles.Add(currentTile.rightNeighbourTile);
            neighbouringTiles.Add(currentTile.leftNeighbourTile);
            neighbouringTiles.Add(currentTile.upperNeighbourTile);
            neighbouringTiles.Add(currentTile.lowerNeighbourTile);

            foreach (var neighbouringTile in neighbouringTiles)
            {
                if(neighbouringTile == null || (!neighbouringTile.walkable && neighbouringTile != selectedTile) || closedList.Contains(neighbouringTile))
                {
                    continue;
                }
                int moveCost = currentTile.gCost + GetManhattanDistance(currentTile, neighbouringTile);
                if (moveCost < neighbouringTile.gCost || !openList.Contains(neighbouringTile))
                {
                    neighbouringTile.gCost = moveCost;
                    neighbouringTile.hCost = GetManhattanDistance(neighbouringTile, selectedTile);
                    neighbouringTile.parent = currentTile;                   
                    if(!openList.Contains(neighbouringTile))
                    {
                        openList.Add(neighbouringTile);
                    }
                } 
            }
        }
        return currentPath;
    }

    private int GetManhattanDistance(Tile a, Tile b)
    {
        int ix = Mathf.Abs(Mathf.RoundToInt(a.transform.position.x - b.transform.position.x));
        int iz = Mathf.Abs(Mathf.RoundToInt(a.transform.position.z - b.transform.position.z));

        int distance = Mathf.Abs(Mathf.RoundToInt((a.transform.position.x - b.transform.position.x) / 4)) + Mathf.Abs(Mathf.RoundToInt((a.transform.position.z - b.transform.position.z) / 4));
        return ix + iz;
    }

    private void GetFinalPath(Tile startingTile, Tile selectedTile, int unitRange)
    {
        List<Tile> finalPath = new List<Tile>();
        Tile currentTile;
        if (!selectedTile.walkable && selectedTile.parent != null)
            currentTile = selectedTile.parent;
        else
            currentTile = selectedTile;
        while(currentTile != startingTile)
        {
            finalPath.Add(currentTile);
            currentTile = currentTile.parent;
        }
        finalPath.Reverse();
        currentPath = finalPath.Count > unitRange ? finalPath.GetRange(0, unitRange) : finalPath;
    }

    private Tile CheckSelectedTile(Tile startingTile, Tile selectedTile)
    {
        if (selectedTile.walkable)
            return selectedTile;
        

        return selectedTile;
    }

    //public List<Tile> EnemiesInRange(Unit unit, LayerMask attackRangeMask)
    //{
    //    List<Tile> tilesWithEnemies = new List<Tile>();
    //    for (int i = 0; i < 360; i++)
    //    {
    //        Vector3 unitPosition = unit.transform.position;
    //        unitPosition.y += 1;
    //        Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
    //        Ray ray = new Ray(unitPosition, q * Vector3.forward);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit, 4 * unit.stats.attackRange.getValue(), attackRangeMask))
    //        {
    //            Unit hitUnit = hit.collider.GetComponent<Unit>();
    //            if (hitUnit != null)
    //            {
    //                if(!tilesWithEnemies.Contains(hitUnit.currentTile) && hitUnit.unitOwner != unit.unitOwner)
    //                tilesWithEnemies.Add(hitUnit.currentTile);
    //            }
    //        }
    //    }
    //    return tilesWithEnemies;
    //}
}
