using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Class for all GameEvents using a single parameter
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GameEvent<T> : GameEvent
{
    // List of Listeners listening to the event in general
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    // List of Listeners listening only to the event when invoked by a certain source object
    private Dictionary<Object, List<GameEventListener<T>>> listenersOnSourceObject = new Dictionary<Object, List<GameEventListener<T>>>();

    /// <summary>
    /// Invokes all listeners without source object and those registered on given source object 
    /// transfering a parameter
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="parameter"></param>
    public virtual void Invoke(Object sourceObject, T parameter)
    {
        if (listenersOnSourceObject.ContainsKey(sourceObject))
        {
            foreach (GameEventListener<T> listener in listenersOnSourceObject[sourceObject])
            {
                listener.OnInvoke(parameter);
            }
        }

        foreach (GameEventListener<T> listener in listeners)
        {
            listener.OnInvoke(parameter);
        }
    }

    /// <summary>
    /// Registers the given Listener to the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(GameEventListener<T> listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters the given Listner from the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void UnregisterListener(GameEventListener<T> listener)
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
    public void RegisterListenerOnSourceObject(Object sourceObject, GameEventListener<T> listener)
    {
        if (listenersOnSourceObject.ContainsKey(sourceObject))
        {
            listenersOnSourceObject[sourceObject].Add(listener);
        }
        else
        {
            listenersOnSourceObject.Add(sourceObject, new List<GameEventListener<T>> { listener });
        }
    }

    /// <summary>
    /// Unregisters the given Listener from the GameEvent called by the given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="listener"></param>
    public void UnregisterListenerOnSourceObject(Object sourceObject, GameEventListener<T> listener)
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
