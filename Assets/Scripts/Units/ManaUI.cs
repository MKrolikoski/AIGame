﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(UnitStats))]
public class ManaUI : MonoBehaviour
{
    public GameObject uiElement;
    public Transform target;
    public bool active;

    private float lastMadeVisibleTime;
    private float visibleTime = 2;

    Transform ui;
    Image manaSlider;
    Transform cam;
    Text text;

    void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiElement, c.transform).transform;
                manaSlider = ui.GetChild(0).GetComponent<Image>();
                text = ui.GetChild(1).GetComponent<Text>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<SkeletonWizardStats>().mp.OnManaChanged += OnManaChanged;
        InitTextField();
        activateManaBar();
    }

    private void InitTextField()
    {
        UnitStats stats = GetComponent<UnitStats>();
        text.text = stats.mp.getValue().ToString() + " / " + stats.mp.baseValue.ToString();
    }

    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
        }
        //if (!active && Time.time - lastMadeVisibleTime > visibleTime && manaSlider.fillAmount > 0)
        //{
        //    deactivateManaBar();
        //}
    }

    public void activateManaBar()
    {
        ui.gameObject.SetActive(true);
        lastMadeVisibleTime = Time.time;
    }

    public void deactivateManaBar()
    {
        ui.gameObject.SetActive(false);
    }

    private void OnManaChanged(int maxMp, int currentMp)
    {
        float healthPerc = currentMp / (float)maxMp;
        manaSlider.fillAmount = healthPerc;
        text.text = currentMp.ToString() + " / " + maxMp.ToString();
    }
}
