using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("Custom/Actions/EndTurn")]
[Help("AI ends their turn")]
public class EndTurn : BasePrimitiveAction
{
    public override TaskStatus OnUpdate()
    {
        Commander activePlayer = GameManager.instance.ActivePlayer;
        if(activePlayer.GetType() == typeof(AI))
        {
            GameManager.instance.EndTurn();
            return TaskStatus.COMPLETED;
        }
        else
        {
            Debug.Log("Current player is not AI");
            return TaskStatus.FAILED;
        }
    }

}
