using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public abstract void Apply(GameObject target);
}
