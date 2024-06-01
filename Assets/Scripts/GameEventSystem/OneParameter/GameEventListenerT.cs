using UnityEngine;

/// <summary>
/// Base Class for all GameEventListeners using a single parameter
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGameEventListener<T>
{
    public void OnInvoke(T parameter);
}
