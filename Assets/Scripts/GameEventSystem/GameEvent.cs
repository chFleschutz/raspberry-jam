using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Class for all GameEvents
/// </summary>
[CreateAssetMenu(fileName = "New GameEvent", menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    // List of Listeners listening to the event in general
    private List<GameEventListener> listeners = new List<GameEventListener>();

    // List of Listeners listening only to the event when invoked by a certain source object
    private Dictionary<Object, List<GameEventListener>> listenersOnSourceObject = new Dictionary<Object, List<GameEventListener>>();

    /// <summary>
    /// Invokes all listeners without source object and those registered on given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    public virtual void Invoke(Object sourceObject)
    {
        if (listenersOnSourceObject.ContainsKey(sourceObject))
        {
            foreach(GameEventListener listener in listenersOnSourceObject[sourceObject])
            {
                listener.OnInvoke();
            }
        }

        foreach (GameEventListener listener in listeners)
        {
            listener.OnInvoke();
        }
    }

    /// <summary>
    /// Registers the given Listener to the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters the given Listner from the GameEvent
    /// </summary>
    /// <param name="listener"></param>
    public void UnregisterListener(GameEventListener listener)
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
    public void RegisterListenerOnSourceObject(Object sourceObject, GameEventListener listener)
    {
        if(listenersOnSourceObject.ContainsKey(sourceObject))
        {
            listenersOnSourceObject[sourceObject].Add(listener);
        }
        else
        {
            listenersOnSourceObject.Add(sourceObject, new List<GameEventListener> { listener });
        }
    }

    /// <summary>
    /// Unregisters the given Listener from the GameEvent called by the given source object
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="listener"></param>
    public void UnregisterListenerOnSourceObject(Object sourceObject, GameEventListener listener)
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
