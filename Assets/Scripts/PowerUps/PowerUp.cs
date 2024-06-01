using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public float Duration = 0;

    public abstract void ApplyTo(GameObject target);
    public abstract void RemoveFrom(GameObject target);
}
