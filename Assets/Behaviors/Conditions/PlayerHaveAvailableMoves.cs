
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase


[Condition("Custom/Conditions/PlayerHaveAvailableMoves")]
[Help("Check whether player have any moves left")]
public class PlayerHaveAvailableMoves : ConditionBase
{
    public override bool Check()
    {
        return GameManager.instance.ActivePlayer.availableMovesLeft > 0;
    }
}