using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoolDownReduction", menuName = "PowerUps/CoolDownReduction")]
public class CoolDownReduction : PowerUp
{
    public float ReductionPercentage;

    public override void ApplyTo(GameObject target)
    {
        if (target.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.CooldownTime *= ReductionPercentage;
        }
    }

    public override void RemoveFrom(GameObject target)
    {
        if (target.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.CooldownTime /= ReductionPercentage;
        }
    }
}
