using UnityEngine;

/// <summary>
/// GameEventListener using a float as paramter
/// </summary>
public abstract class GameEventListenerFloat : GameEventListener<float>
{
    public abstract override void OnInvoke(float parameter);
}
