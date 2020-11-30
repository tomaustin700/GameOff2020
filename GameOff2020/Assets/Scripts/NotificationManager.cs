using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class NotificationManager
{
    public static List<NotificationEvent> Notifications = new List<NotificationEvent>();
    private static List<NotificationEvent> NotificationHistory = new List<NotificationEvent>();

    public static void AddNotification(NotificationEvent notification)
    {
        if(!Notifications.Any(x => x.EventName == notification.EventName) && !NotificationHistory.Any(x => x.EventName == notification.EventName))
        {
            Notifications.Add(notification);
        }
    }
    public static void UpdateNotification(NotificationEvent notification)
    {
        if (Notifications.Any(x => x.EventName == notification.EventName &&  x.IsReady))
        {
            var notificationToUpdate = Notifications.First(x => x.EventName == notification.EventName);
            //notificationToUpdate.Title = notification.Title;
            notificationToUpdate.Description = notification.Description;
            notificationToUpdate.CurrentCount = notification.CurrentCount;
            notificationToUpdate.MaxCount = notification.MaxCount;
        }
        else
        {
            Notifications.Add(notification);
        }
    }

    public static void RemoveAllNotification()
    {
        Notifications = new List<NotificationEvent>();
    }
    public static void CompleteNotification(EventName eventName,bool completeIfNotActive = false)
    {
        if (!NotificationHistory.Any(x => x.EventName == eventName))
        {
            var currentNotification = completeIfNotActive ? Notifications.FirstOrDefault(x => x.EventName == eventName) : Notifications.FirstOrDefault();
            if (currentNotification != null && currentNotification.EventName == eventName && (completeIfNotActive || currentNotification.IsReady))
            {
                Notifications.RemoveAll(x => x.EventName == eventName);
                NotificationHistory.Add(currentNotification);
            }
            else if (completeIfNotActive)
            {
                NotificationHistory.Add(new NotificationEvent(eventName,$"Pre-completed {eventName}"));
            }
        }
    }
}
