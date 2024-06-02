using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FuelPowerUp", menuName = "PowerUps/FuelPowerUp")]
public class FuelPowerUp : PowerUp
{
    public float fuelAmount;

    public override void ApplyTo(GameObject target)
    {
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        player.AddFuel(fuelAmount);
    }

    public override void RemoveFrom(GameObject target)
    {
    }
}
