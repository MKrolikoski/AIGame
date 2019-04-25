using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(UnitStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiElement;
    public Transform target;
    public bool active;

    private float lastMadeVisibleTime;
    private float visibleTime = 2;

    Transform ui;
    Image healthSlider;
    Transform cam;
    Text text;

	void Start ()
    {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiElement, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                text = ui.GetChild(1).GetComponent<Text>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<UnitStats>().hp.OnHealthChanged += OnHealthChanged;
        InitTextField();
        activateHealthBar();

    }

    private void InitTextField()
    {
        UnitStats stats = GetComponent<UnitStats>();
        text.text = stats.hp.getValue().ToString() + " / " + stats.hp.baseValue.ToString();
    }

    void LateUpdate ()
    {
        if(ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
        }
        //if(!active && Time.time - lastMadeVisibleTime > visibleTime && healthSlider.fillAmount > 0)
        //{
        //    deactivateHealthBar();
        //}
	}

    public void activateHealthBar()
    {
        ui.gameObject.SetActive(true);
        lastMadeVisibleTime = Time.time;
    }

    public void deactivateHealthBar()
    {
        ui.gameObject.SetActive(false);
    }

    private void OnHealthChanged(int maxHp, int currentHp)
    {
        float healthPerc = currentHp / (float) maxHp;
        healthSlider.fillAmount = healthPerc;
        text.text = currentHp.ToString() + " / " + maxHp.ToString();
        if (currentHp <= 0)
        {
            Destroy(ui.gameObject);
        }
    }
}
