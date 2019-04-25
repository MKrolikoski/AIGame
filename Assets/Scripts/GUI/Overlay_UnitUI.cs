using UnityEngine;
using UnityEngine.UI;

public class Overlay_UnitUI : Overlay_Element
{
    public Image healthSlider;
    public Text healthPoints;

    public Image manaSlider;
    public Text manaPoints;

    public Text unitName;

    private Unit unit;

    private void Update()
    {
        if (unit != null)
        {
            UpdateElement(unit);
        }
    }

    public override void UpdateElement(Unit selectedUnit)
    {
        base.UpdateElement(selectedUnit);

        UpdateHealthBar(selectedUnit.stats.hp.baseValue, selectedUnit.stats.hp.getValue());
        UpdateManaBar(selectedUnit.stats.mp.baseValue, selectedUnit.stats.mp.getValue());
        UpdateUnitName(selectedUnit.unitName);

        unit = selectedUnit;
    }

    private void UpdateHealthBar(int maxHp, int currentHp)
    {
        float healthPerc = currentHp / (float)maxHp;
        healthSlider.fillAmount = healthPerc;
        healthPoints.text = currentHp.ToString() + " / " + maxHp.ToString();
    }

    private void UpdateManaBar(int maxMp, int currentMp)
    {
        if (maxMp == 0)
        {
             if(manaSlider.IsActive())
                 manaSlider.gameObject.SetActive(false);
        }
        else
        {
            if(!manaSlider.IsActive())
                manaSlider.gameObject.SetActive(true);
            float healthPerc = currentMp / (float)maxMp;
            manaSlider.fillAmount = healthPerc;
            manaPoints.text = currentMp.ToString() + " / " + maxMp.ToString();
        }
    }

    private void UpdateUnitName(string name)
    {
        unitName.text = name;
    }
}
