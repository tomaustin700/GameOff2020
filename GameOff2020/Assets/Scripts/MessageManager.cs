using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MessageManager
{
    public static Message LatestMessage;

    public static void AddMessage(Message message)
    {
        LatestMessage = message;
    }

}
