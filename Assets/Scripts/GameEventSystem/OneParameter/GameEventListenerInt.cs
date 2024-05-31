using UnityEngine;

/// <summary>
/// GameEventListener using a Int as paramter
/// </summary>
public abstract class GameEventListenerInt : GameEventListener<int>
{
    public abstract override void OnInvoke(int parameter);
}
