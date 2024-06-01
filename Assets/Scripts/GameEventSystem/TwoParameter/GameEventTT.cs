using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Class for all GameEvents using two parameters
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public abstract class GameEvent<T1, T2> : GameEvent
{
    // List of Listeners listening to the event in general
    private List<IGameEventListener<T1, T2>> listeners = new();

    // List of Listeners listening only to the event when invoked by a certain source object
    private Dictionary<Object, List<IGameEventListener<T1, T2>>> listenersOnSourceObject = new();

    /// <summary>
    /// Invokes all listeners without source object and those registered on given source object 
    /// transfering two parameters
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="parameter1"></param>
    /// <param name="parameter2"></param>
    public virtual void Invoke(Object sourceObject, T1 parameter1, T2 parameter2)
    {
        if (listenersOnSourceObject.ContainsKey(sourceObject))
        {
            foreach (IGameEventListener<T1, T2> listener in listenersOnSourceObject[sourceObject])
            {
                listener.OnInvoke(parameter1, parameter2);
            }
        }

        foreach (IGameEventListener<T1, T2> listener in listeners)
        {
            listener.OnInvoke(parameter1, parameter2);
        }
    }

    /// <summary>
    /// Registers the given Listener to the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(IGameEventListener<T1, T2> listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters the given Listner from the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void UnregisterListener(IGameEventListener<T1, T2> listener)
    {
        if (!listeners.Contains(listener))
        {
            return;
        }

        listeners.Remove(listener);
    }

    /// <summary>
    /// Registers the given Listener to the GameEvent, but only listens if invoked by the given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="listener"></param>
    public void RegisterListenerOnSourceObject(Object sourceObject, IGameEventListener<T1, T2> listener)
    {
        if (listenersOnSourceObject.ContainsKey(sourceObject))
        {
            listenersOnSourceObject[sourceObject].Add(listener);
        }
        else
        {
            listenersOnSourceObject.Add(sourceObject, new List<IGameEventListener<T1, T2>> { listener });
        }
    }

    /// <summary>
    /// Unregisters the given Listener from the GameEvent called by the given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="listener"></param>
    public void UnregisterListenerOnSourceObject(Object sourceObject, IGameEventListener<T1, T2> listener)
    {
        if (!listenersOnSourceObject.ContainsKey(sourceObject))
        {
            return;
        }

        if (!listenersOnSourceObject[sourceObject].Contains(listener))
        {
            return;
        }

        listenersOnSourceObject[sourceObject].Remove(listener);

        if (listenersOnSourceObject[sourceObject].Count <= 0)
        {
            listenersOnSourceObject.Remove(sourceObject);
        }
    }
}
