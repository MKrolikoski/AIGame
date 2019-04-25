
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase


[Condition("Custom/Conditions/IsXTT2AITurn")]
[Help("Check whether XTT2AI is the active player")]
public class IsXTT2AITurn : ConditionBase
{
    public override bool Check()
    {
        Commander activePlayer = GameManager.instance.ActivePlayer;
        return activePlayer.GetType() == typeof(XTT2AI);
    }
}