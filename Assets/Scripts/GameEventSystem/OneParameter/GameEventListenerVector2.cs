using UnityEngine;

/// <summary>
/// GameEventListener using a Vector2 as paramter
/// </summary>
public abstract class GameEventListenerVector2 : GameEventListener<Vector2>
{
    public abstract override void OnInvoke(Vector2 parameter);
}
