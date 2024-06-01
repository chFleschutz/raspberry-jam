using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slomo", menuName = "PowerUps/Slomo")]
public class Slomo : PowerUp
{
    public float TimeScale;

    public override void ApplyTo(GameObject target)
    {
        Time.timeScale = TimeScale;
    }

    public override void RemoveFrom(GameObject target)
    {
        Time.timeScale = 1;
    }
}
