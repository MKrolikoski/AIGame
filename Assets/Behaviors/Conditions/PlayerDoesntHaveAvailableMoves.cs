
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using UnityEngine;

[Condition("Custom/Conditions/PlayerDoesntHaveAvailableMoves")]
[Help("Check whether player does not have any moves left")]
public class PlayerDoesntHaveAvailableMoves : ConditionBase
{
    public override bool Check()
    {
        return GameManager.instance.ActivePlayer.availableMovesLeft == 0;
    }
}

