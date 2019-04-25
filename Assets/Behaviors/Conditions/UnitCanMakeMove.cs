
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase


[Condition("Custom/Conditions/UnitCanMakeMove")]
[Help("Check whether selected unit can make move")]
public class UnitCanMakeMove : ConditionBase
{
    [InParam("selectedUnit")]
    public Unit selectedUnit;

    public override bool Check()
    {
        if (selectedUnit == null || GameManager.instance.ActivePlayer.availableMovesLeft == 0)
            return false;
        return selectedUnit.canBeSelected;
    }
}