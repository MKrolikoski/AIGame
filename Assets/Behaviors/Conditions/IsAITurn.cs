
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase


[Condition("Custom/Conditions/IsAITurn")]
[Help("Check whether AI is the active player")]
public class IsAITurn : ConditionBase
{
    public override bool Check()
    {
        Commander activePlayer = GameManager.instance.ActivePlayer;
        return activePlayer.GetType() == typeof(AI);
    }
}