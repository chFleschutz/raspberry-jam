using UnityEngine;

/// <summary>
/// Base Class for all GameEventListeners using two parameters
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public abstract class GameEventListener<T1,T2> : MonoBehaviour
{
    public abstract void OnInvoke(T1 parameter1, T2 paramter2);
}
