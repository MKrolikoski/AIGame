using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    public void EndTurn()
    {
        if(InputController.instance.inputEnabled)
            GameManager.instance.EndTurn();
    }

    public void Show()
    {
        GetComponent<Animator>().Play("end_turn_button_show");
    }

    public void Hide()
    {
        GetComponent<Animator>().Play("end_turn_button_hide");
    }

    public void ActivateButton()
    {
        button.interactable = true;
    }

    public void DeactivateButton()
    {
        button.interactable = false;
    }
}
