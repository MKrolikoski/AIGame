using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("TestConditions/TestCondition")]
[Help("Checks stuff")]
public class TestCondition : ConditionBase
{
    [InParam("Destination Tile")]
    public Tile destinationTile;

    public override bool Check()
    {
        if (destinationTile.unitOnTile == null)
            return true;
        return false;
    }
}
