using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{

    public GameObject messagePanel;
    public Text messageTitle;
    public Text messageBody;


    void Update()
    {
        var latestMessage = MessageManager.LatestMessage;
        if (latestMessage != null)
        {
            messageTitle.text = latestMessage.Title;
            messageBody.text = latestMessage.Body;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.FirstMissionControlMessage))
            {
                NotificationManager.CompleteNotification(EventName.FirstMissionControlMessage);
                NotificationManager.AddNotification(new NotificationEvent(EventName.Explore, "Explore The Landscape Whilst Awaiting Instructions"));

            }

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.SecondMissionControlMessage))
            {
                NotificationManager.CompleteNotification(EventName.SecondMissionControlMessage);
                NotificationManager.AddNotification(new NotificationEvent(EventName.CraftDrill, "Craft Drill and Place"));

            }

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.ThirdMissionControlMessage))
            {
                NotificationManager.CompleteNotification(EventName.ThirdMissionControlMessage);
                NotificationManager.AddNotification(new NotificationEvent(EventName.MineOxygen, "Mine Oxygen to Extend Your Suit Range"));

            }

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.ForthMissionControlMessage))
            {
                NotificationManager.CompleteNotification(EventName.ForthMissionControlMessage);

                var gm = FindObjectOfType<GameManager>();
                gm.StartDropSpawn();

            }

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.FifthMissionControlMessage))
            {
                NotificationManager.CompleteNotification(EventName.FifthMissionControlMessage);
                NotificationManager.AddNotification(new NotificationEvent(EventName.OpenDrop, "Find Supply Drop And Open"));


            }





            if (!messagePanel.activeSelf)
            {
                if (latestMessage != null)
                    NotificationManager.CompleteNotification(latestMessage.EventName);

                messagePanel.SetActive(true);

            }
            else
                messagePanel.SetActive(false);

        }


    }
}
