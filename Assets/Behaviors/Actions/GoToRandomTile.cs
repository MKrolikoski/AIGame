using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections.Generic;

[Action("Custom/Actions/GoToRandomTile")]
[Help("Selected unit goes to random tile")]
public class GoToRandomTile : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    public override TaskStatus OnUpdate()
    {
        List<Tile> tilesToMove = new List<Tile>();
        tilesToMove.Add(selectedUnit.currentTile.upperNeighbourTile);
        selectedUnit.MoveThroughTiles(tilesToMove);
        return TaskStatus.COMPLETED;
    }

}
