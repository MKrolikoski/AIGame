
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/IsUnitSelected")]
[Help("Check whether any unit is selected")]
public class IsUnitSelected : ConditionBase
{
    public override bool Check()
    {
        //Debug.Log("IsUnitSelected:" + (GameManager.instance.SelectedUnit != null));
        return GameManager.instance.SelectedUnit != null;
    }
}