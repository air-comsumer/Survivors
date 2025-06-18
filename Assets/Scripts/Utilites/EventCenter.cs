using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IEventInfo
{
}
public class EventInfo : IEventInfo
{
    public UnityAction action;
    public EventInfo(UnityAction action)
    {
        this.action += action;
    }
}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;
    public EventInfo(UnityAction<T> action)
    {
        this.action += action;
    }
}
public class EventInfo<T1, T2> : IEventInfo
{
    public UnityAction<T1, T2> action;
    public EventInfo(UnityAction<T1, T2> action)
    {
        this.action += action;
    }
}
public class EventInfo<T1, T2, T3> : IEventInfo
{
    public UnityAction<T1, T2, T3> action;
    public EventInfo(UnityAction<T1, T2, T3> action)
    {
        this.action += action;
    }
}
public class EventInfo<T1, T2, T3, T4> : IEventInfo
{
    public UnityAction<T1, T2, T3, T4> action;
    public EventInfo(UnityAction<T1, T2, T3, T4> action)
    {
        this.action += action;
    }
}
public class EventCenter:MonoBehaviour
{
    private static EventCenter instance;
    public static EventCenter Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("EventCenter");
                instance = obj.AddComponent<EventCenter>();
                DontDestroyOnLoad(obj); // 保持在场景切换时不销毁
            }
            return instance;
        }
    }
    public Dictionary<string,IEventInfo> eventCenter = new Dictionary<string, IEventInfo>();
    public void AddListener(string eventName, UnityAction action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfo(action));
        }
    }
    public void RemoveListener(string eventName, UnityAction action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo).action -= action;
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void EventTrigger(string eventName)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo).action?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }

    public void AddListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfo<T>(action));
        }
    }
    public void RemoveListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T>).action -= action;
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void EventTrigger<T>(string eventName, T arg)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T>).action?.Invoke(arg);
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void AddListener<T1, T2>(string eventName, UnityAction<T1, T2> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfo<T1, T2>(action));
        }
    }
    public void RemoveListener<T1, T2>(string eventName, UnityAction<T1, T2> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2>).action -= action;
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void EventTrigger<T1, T2>(string eventName, T1 arg1, T2 arg2)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2>).action?.Invoke(arg1, arg2);
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }

    public void AddListener<T1, T2, T3>(string eventName, UnityAction<T1, T2, T3> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2, T3>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfo<T1, T2, T3>(action));
        }
    }
    public void RemoveListener<T1, T2, T3>(string eventName, UnityAction<T1, T2, T3> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2, T3>).action -= action;
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void EventTrigger<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2, T3>).action?.Invoke(arg1, arg2, arg3);
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void AddListener<T1, T2, T3, T4>(string eventName, UnityAction<T1, T2, T3, T4> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2, T3, T4>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfo<T1, T2, T3, T4>(action));
        }
    }
    public void RemoveListener<T1, T2, T3, T4>(string eventName, UnityAction<T1, T2, T3, T4> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2, T3, T4>).action -= action;
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
    public void EventTrigger<T1, T2, T3, T4>(string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfo<T1, T2, T3, T4>).action?.Invoke(arg1, arg2, arg3, arg4);
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
}
