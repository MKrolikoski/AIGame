using UnityEngine;
using UnityEngine.UI;

public class MovesUI : MonoBehaviour
{
    public Text availableMovesText;

    private void Start()
    {
        GameManager.instance.OnUnitEndedMove += UpdateElement;
        GameManager.instance.OnNewTurn += UpdateElement;
    }

    public void UpdateElement()
    {
        availableMovesText.text = GameManager.instance.ActivePlayer.availableMovesLeft.ToString();
    }
}
