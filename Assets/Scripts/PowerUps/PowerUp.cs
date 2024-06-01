using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public string Name = "New PowerUp";
    public float Duration = 0;
    public Sprite Icon;

    public abstract void ApplyTo(GameObject target);
    public abstract void RemoveFrom(GameObject target);
}
