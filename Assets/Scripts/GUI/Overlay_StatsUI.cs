using UnityEngine;
using UnityEngine.UI;

public class Overlay_StatsUI : Overlay_Element {

    public Text attackValue;
    public Text defenceValue;
    public Text attackRangeValue;
    public Text moveRangeValue;
    public Transform leechParent;
    public Text leechValue;
    private Unit unit;


    void Start ()
    {
		
	}
	
	void Update ()
    {
		if(unit != null)
        {
            UpdateElement(unit);
        }
	}

    public override void UpdateElement(Unit selectedUnit)
    {
        base.UpdateElement(selectedUnit);

        attackValue.text = selectedUnit.stats.attack.getValue().ToString();
        defenceValue.text = selectedUnit.stats.defence.getValue().ToString();
        attackRangeValue.text = selectedUnit.stats.attackRange.getValue().ToString();
        moveRangeValue.text = selectedUnit.stats.moveRange.getValue().ToString();
        int leechStat = selectedUnit.stats.leech.getValue();
        if (leechStat != 0)
        {
            leechParent.gameObject.SetActive(true);
            leechValue.text = leechStat.ToString() + "%";
        }
        else
        {
            leechParent.gameObject.SetActive(false);
        }
        unit = selectedUnit;
    }
}
