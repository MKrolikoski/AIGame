using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWizard : Unit
{

    protected override void Awake()
    {
        base.Awake();
        unitName = "Skeleton Wizard";
    }

    protected override void Start()
    {
        base.Start();

    }


    public override void OnSelect()
    {
        base.OnSelect();
        ShowMp();
        GetComponent<ManaUI>().active = true;
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        //HideMp();
        GetComponent<ManaUI>().active = false;
    }

    public void ShowMp()
    {
        GetComponent<ManaUI>().activateManaBar();
    }

    public void HideMp()
    {
        GetComponent<ManaUI>().deactivateManaBar();
    }

    void Update()
    {
    }
}
