using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationEvent
{
    public EventName EventName { get; }
    //  public string Title;
    public string Description;
    public int CurrentCount = 0;
    public int MaxCount = 1;
    public bool IsReady = false;
    public NotificationEvent(EventName _eventName, string _description)
    {
        EventName = _eventName;
        Description = _description;
    }
    public NotificationEvent(EventName _eventName, string _description, int _maxCount)
    {
        EventName = _eventName;
        Description = _description;
        MaxCount = _maxCount;
    }

}
