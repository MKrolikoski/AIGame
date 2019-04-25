using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberUI : MonoBehaviour {

    public DamageNumber damageNumber;
    private Canvas canvas;

    void Start()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                canvas = c;
                break;
            }
        }
    }

    public void CreateDamageNumber(string number, Transform location)
    {
        DamageNumber instance = Instantiate(damageNumber);
        instance.transform.SetParent(canvas.transform, false);
        instance.setLocation(location);
        instance.SetText(number);
    }
}
