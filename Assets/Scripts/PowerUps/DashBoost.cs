using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashBoost", menuName = "PowerUps/DashBoost")]
public class DashBoost : PowerUp
{
    public float boostPercentage = 1f;

    public override void ApplyTo(GameObject target)
    {
        if (target.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.ChargePower *= boostPercentage;
        }
    }

    public override void RemoveFrom(GameObject target)
    {
        if (target.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.ChargePower /= boostPercentage;
        }
    }
}
