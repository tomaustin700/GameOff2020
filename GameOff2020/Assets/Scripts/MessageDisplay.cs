using System.Collections;
using System.Collections.Generic;
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
