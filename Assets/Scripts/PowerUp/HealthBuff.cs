using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthBuff", menuName = "PowerUps/HealthBuff")]
public class HealthBuff : PowerUp
{
    public int HealthIncrease = 0;

    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent<Health>(out var health))
        {
            health.IncreaseHealth(HealthIncrease);
        }
    }
}
