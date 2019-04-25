using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationEventReceiver : MonoBehaviour
{
    private Unit unit;
    private UnitCombat unitCombat;

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        unitCombat = GetComponentInParent<UnitCombat>();
    }

    public void DeathEvent()
    {
        unit.Death_AnimationEvent();
    }

    public void AttackHitEvent()
    {
        unitCombat.AttackHit_AnimationEvent();
    }


}
