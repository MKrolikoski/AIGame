
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/IsUnitNotSelected")]
[Help("Check whether there is no selected unit")]
public class IsUnitNotSelected : ConditionBase
{
    public override bool Check()
    {
        //Debug.Log("IsUnitNotSelected:" + (GameManager.instance.SelectedUnit == null));
        return GameManager.instance.SelectedUnit == null;
    }
}