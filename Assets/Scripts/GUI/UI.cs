
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;
    private GameManager gameManager;
    public Overlay_Element[] overlayElements;
    public Transform overlay_parent;
    private float lastMadeVisibleTime;
    private float visibleTime = 1;
    private bool active = false;


    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start ()
    {
        gameManager = GameManager.instance;
        overlayElements = GetComponentsInChildren<Overlay_Element>();
        gameManager.OnUnitSelected += UpdateUI;
        HideUI();
    }

    private void Update()
    {
        if (Time.time - lastMadeVisibleTime > visibleTime && !active)
        {
            HideUI();
        }
    }

    public void UpdateUI(Unit selectedUnit)
    {
        if (selectedUnit != null)
        {
            active = true;
            lastMadeVisibleTime = Time.time;
            if (!overlay_parent.gameObject.activeSelf)
            {
                ShowUI();
            }
            foreach (Overlay_Element overlayElement in overlayElements)
            {
                overlayElement.UpdateElement(selectedUnit);
            }
        }
        else
        {
            active = false;
            lastMadeVisibleTime = Time.time;
        }
    }

    public void HideUI()
    {
        overlay_parent.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        overlay_parent.gameObject.SetActive(true);
    }
}
