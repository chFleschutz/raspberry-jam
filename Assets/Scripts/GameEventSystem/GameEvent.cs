using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Class for all GameEvents
/// </summary>
[CreateAssetMenu(fileName = "New GameEvent", menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    // List of Listeners listening to the event in general
    private List<IGameEventListener> listeners = new();

    // List of Listeners listening only to the event when invoked by a certain source object
    private Dictionary<Object, List<IGameEventListener>> listenersOnSourceObject = new();

    /// <summary>
    /// Invokes all listeners without source object and those registered on given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    public virtual void Invoke(Object sourceObject)
    {
        if (listenersOnSourceObject.ContainsKey(sourceObject))
        {
            foreach(IGameEventListener listener in listenersOnSourceObject[sourceObject])
            {
                listener.OnInvoke();
            }
        }

        foreach (IGameEventListener listener in listeners)
        {
            listener.OnInvoke();
        }
    }

    /// <summary>
    /// Registers the given Listener to the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(IGameEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters the given Listner from the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void UnregisterListener(IGameEventListener listener)
    {
        if(!listeners.Contains(listener))
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
    public void RegisterListenerOnSourceObject(Object sourceObject, IGameEventListener listener)
    {
        if(listenersOnSourceObject.ContainsKey(sourceObject))
        {
            listenersOnSourceObject[sourceObject].Add(listener);
        }
        else
        {
            listenersOnSourceObject.Add(sourceObject, new List<IGameEventListener> { listener });
        }
    }

    /// <summary>
    /// Unregisters the given Listener from the GameEvent called by the given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="listener"></param>
    public void UnregisterListenerOnSourceObject(Object sourceObject, IGameEventListener listener)
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
