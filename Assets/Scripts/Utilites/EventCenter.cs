using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IEventInfomation 
{
}
public class EventInfomation: IEventInfomation 
{
    public UnityAction action;
    public EventInfomation(UnityAction action)
    {
        this.action += action;
    }
}
public class EventInfomation<T> : IEventInfomation 
{
    public UnityAction<T> action;
    public EventInfomation(UnityAction<T> action)
    {
        this.action += action;
    }
}
public class EventInfomation <T1, T2> : IEventInfomation 
{
    public UnityAction<T1, T2> action;
    public EventInfomation (UnityAction<T1, T2> action)
    {
        this.action += action;
    }
}
public class EventInfomation <T1, T2, T3> : IEventInfomation 
{
    public UnityAction<T1, T2, T3> action;
    public EventInfomation (UnityAction<T1, T2, T3> action)
    {
        this.action += action;
    }
}
public class EventInfomation <T1, T2, T3, T4> : IEventInfomation 
{
    public UnityAction<T1, T2, T3, T4> action;
    public EventInfomation (UnityAction<T1, T2, T3, T4> action)
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
                DontDestroyOnLoad(obj); // �����ڳ����л�ʱ������
            }
            return instance;
        }
    }
    public Dictionary<string,IEventInfomation > eventCenter = new Dictionary<string, IEventInfomation >();
    public void AddListener(string eventName, UnityAction action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfomation  ).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfomation(action));
        }
    }
    public void RemoveListener(string eventName, UnityAction action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfomation).action -= action;
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
            (eventCenter[eventName] as EventInfomation ).action?.Invoke();
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
            (eventCenter[eventName] as EventInfomation<T>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfomation<T>(action));
        }
    }
    public void RemoveListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfomation<T>).action -= action;
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
            (eventCenter[eventName] as EventInfomation<T>).action?.Invoke(arg);
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
            (eventCenter[eventName] as EventInfomation <T1, T2>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfomation <T1, T2>(action));
        }
    }
    public void RemoveListener<T1, T2>(string eventName, UnityAction<T1, T2> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfomation <T1, T2>).action -= action;
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
            (eventCenter[eventName] as EventInfomation <T1, T2>).action?.Invoke(arg1, arg2);
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
            (eventCenter[eventName] as EventInfomation <T1, T2, T3>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfomation <T1, T2, T3>(action));
        }
    }
    public void RemoveListener<T1, T2, T3>(string eventName, UnityAction<T1, T2, T3> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfomation <T1, T2, T3>).action -= action;
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
            (eventCenter[eventName] as EventInfomation <T1, T2, T3>).action?.Invoke(arg1, arg2, arg3);
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
            (eventCenter[eventName] as EventInfomation <T1, T2, T3, T4>).action += action;
            return;
        }
        else
        {
            eventCenter.Add(eventName, new EventInfomation <T1, T2, T3, T4>(action));
        }
    }
    public void RemoveListener<T1, T2, T3, T4>(string eventName, UnityAction<T1, T2, T3, T4> action)
    {
        if (eventCenter.ContainsKey(eventName))
        {
            (eventCenter[eventName] as EventInfomation <T1, T2, T3, T4>).action -= action;
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
            (eventCenter[eventName] as EventInfomation <T1, T2, T3, T4>).action?.Invoke(arg1, arg2, arg3, arg4);
        }
        else
        {
            Debug.LogWarning($"Event {eventName} does not exist.");
        }
    }
}
