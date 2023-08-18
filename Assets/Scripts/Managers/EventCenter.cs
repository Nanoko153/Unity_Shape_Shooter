using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : PersistentSingleton<EventCenter>
{
    //�ֵ�洢�¼��б�
    private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>();

    protected override void Awake()
    {
        base.Awake();
    }

    #region �޲η���
    public void AddEventListener(string name, UnityAction action)
    {
        if(_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            _eventDic.Add(name, new EventInfo(action));
        }
    }

    public void EventTrigger(string name)
    {
        if(_eventDic.ContainsKey(name))
        {
            if((_eventDic[name] as EventInfo).actions != null)
            {
                (_eventDic[name] as EventInfo).actions.Invoke();
            }
        }
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if(_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo).actions -= action;
        }
    }
    #endregion

    #region �вη���
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if(_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            _eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    public void EventTrigger<T>(string name, T info)
    {
        if(_eventDic.ContainsKey(name))
        {
            if((_eventDic[name] as EventInfo<T>).actions != null)
            {
                (_eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if(_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions -= action;
        }
    }
    #endregion



    public void Clear()
    {
        _eventDic.Clear();
    }
}

public interface IEventInfo
{

}

public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}