using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    PLAYER_ATTACKED = 0
}

public static class EventManager<T>
{
    private static Dictionary<EventType, System.Action<T>> eventDictionary = new Dictionary<EventType, System.Action<T>>();

    public static void Subscribe(EventType _type, System.Action<T> _function)
    {
        if (!eventDictionary.ContainsKey(_type))
        {
            eventDictionary.Add(_type, null);
        }
        eventDictionary[_type] += _function;
    }
    public static void UnSubscribe(EventType _type, System.Action<T> _function)
    {
        if (eventDictionary.ContainsKey(_type) && eventDictionary[_type] != null)
        {
            eventDictionary[_type] -= _function;
        }
    }
    public static void Invoke(EventType _type, T _arg1)
    {
        eventDictionary[_type]?.Invoke(_arg1);
    }
}
