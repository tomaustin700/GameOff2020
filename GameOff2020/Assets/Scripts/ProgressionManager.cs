using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NotificationManager.AddNotification(new NotificationEvent(EventName.MoveForwards, "Move forwards with W"));
        NotificationManager.AddNotification(new NotificationEvent(EventName.MoveBackwards, "Move backwards with S"));
        NotificationManager.AddNotification(new NotificationEvent(EventName.TurnLeft, "Turn left with A"));
        NotificationManager.AddNotification(new NotificationEvent(EventName.TurnRight, "Turn right with D"));
        NotificationManager.AddNotification(new NotificationEvent(EventName.Jump, "Jump with Space"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
