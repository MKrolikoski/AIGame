using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

    public Text text;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }


    public void Activate(Commander winner)
    {
        text.text = winner.name + " Won";
        gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        animator.SetTrigger("GameOver");
    }

    public void GameOverTextPanelAnimEnd()
    {
        //gameObject.SetActive(false);
        //text.gameObject.SetActive(false);
        //TODO
    }
}
