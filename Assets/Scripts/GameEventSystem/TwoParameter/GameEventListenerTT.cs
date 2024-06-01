using UnityEngine;

/// <summary>
/// Base Class for all GameEventListeners using two parameters
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public interface IGameEventListener<T1,T2>
{
    public void OnInvoke(T1 parameter1, T2 paramter2);
}
