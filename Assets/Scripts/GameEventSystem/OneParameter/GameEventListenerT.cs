using UnityEngine;

/// <summary>
/// Base Class for all GameEventListeners using a single parameter
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GameEventListener<T> : MonoBehaviour
{
    public abstract void OnInvoke(T parameter);
}
