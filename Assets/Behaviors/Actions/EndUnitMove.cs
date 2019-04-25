using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

[Action("Custom/Actions/EndUnitMove")]
[Help("Ends selected unit turn")]
public class EndUnitMove : BasePrimitiveAction
{

    public override TaskStatus OnUpdate()
    {
        //Debug.Log("Ending selected unit move");
        return TaskStatus.COMPLETED;
    }
}
