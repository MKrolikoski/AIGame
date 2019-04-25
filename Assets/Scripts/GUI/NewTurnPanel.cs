using UnityEngine;
using UnityEngine.UI;

public class NewTurnPanel : MonoBehaviour
{
    [HideInInspector]
    public Commander activePlayer;
    public Text text;
    public Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void SetActivePlayer(Commander activePlayer)
    {
        this.activePlayer = activePlayer;
        text.text = activePlayer.name + " Turn";
        gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        if (animator != null)
            animator.SetTrigger("NewTurn");
        else
            Debug.Log("Animator is null");
    }

    public void NewTurnTextPanelAnimEnd()
    {
        gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        GameManager.instance.StartTurn();
        GameManager.instance.ShowEndTurnButton();
    }
}
