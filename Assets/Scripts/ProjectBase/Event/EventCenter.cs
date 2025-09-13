using System;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : BaseSingleton<EventCenter>
{
    private Dictionary<string, Action> m_Events = new Dictionary<string, Action>();
    private Dictionary<string, Delegate> m_EventsWithParam = new Dictionary<string, Delegate>();

    public void Subscribe(string eventName, Action callback)
    {
        if (m_Events.TryGetValue(eventName, out Action action))
        {
            action += callback;
            m_Events[eventName] = action;
        }
        else
        {
            m_Events[eventName] = callback;
        }
    }

    public void Unsubscribe(string eventName, Action callback)
    {
        if (m_Events.TryGetValue(eventName, out Action action))
        {
            action -= callback;
            if (action == null)
            {
                m_Events.Remove(eventName);
            }
            else
            {
                m_Events[eventName] = action;
            }
        }
    }

    public void Publish(string eventName)
    {
        if (m_Events.TryGetValue(eventName, out Action action))
        {
            action?.Invoke();
        }
    }

    public void Subscribe<T>(string eventName, Action<T> callback)
    {
        string fullEventName = $"{eventName}_{typeof(T).Name}";
        if (m_EventsWithParam.TryGetValue(fullEventName, out Delegate del))
        {
            m_EventsWithParam[fullEventName] = Delegate.Combine(del, callback);
        }
        else
        {
            m_EventsWithParam[fullEventName] = callback;
        }
    }

    public void Unsubscribe<T>(string eventName, Action<T> callback)
    {
        string fullEventName = $"{eventName}_{typeof(T).Name}";
        if (m_EventsWithParam.TryGetValue(fullEventName, out Delegate del))
        {
            Delegate newDel = Delegate.Remove(del, callback);
            if (newDel == null)
            {
                m_EventsWithParam.Remove(fullEventName);
            }
            else
            {
                m_EventsWithParam[fullEventName] = newDel;
            }
        }
    }

    public void Publish<T>(string eventName, T param)
    {
        string fullEventName = $"{eventName}_{typeof(T).Name}";
        if (m_EventsWithParam.TryGetValue(fullEventName, out Delegate del))
        {
            (del as Action<T>)?.Invoke(param);
        }
    }

    public void ClearAllEvents()
    {
        m_Events.Clear();
        m_EventsWithParam.Clear();
    }

    public void CleanupNullCallbacks()
    {
        List<string> emptyEvents = new List<string>();
        foreach (var pair in m_Events)
        {
            if (pair.Value == null)
            {
                emptyEvents.Add(pair.Key);
            }
        }
        foreach (string eventName in emptyEvents)
        {
            m_Events.Remove(eventName);
        }

        List<string> emptyParamEvents = new List<string>();
        foreach (var pair in m_EventsWithParam)
        {
            if (pair.Value == null)
            {
                emptyParamEvents.Add(pair.Key);
            }
        }
        foreach (string eventName in emptyParamEvents)
        {
            m_EventsWithParam.Remove(eventName);
        }
    }
}