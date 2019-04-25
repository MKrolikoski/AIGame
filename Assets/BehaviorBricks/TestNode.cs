using UnityEngine;
using Pada1.BBCore;          
using Pada1.BBCore.Tasks;    
using Pada1.BBCore.Framework; 

[Action("TestActions/TestNode")]
[Help("Test action that does stuff.")]
public class TestNode : BasePrimitiveAction
{
    [InParam("SelectedUnit")]
    public Unit selectedUnit;

    [InParam("DestinationTile")]
    public Tile destinationTile;

    public override TaskStatus OnUpdate()
    {
        selectedUnit.MoveToPoint(destinationTile.transform.position);
        return TaskStatus.COMPLETED;
    }
}
