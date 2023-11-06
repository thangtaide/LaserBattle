using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifiUntils
{
    public static Queue<MessNotifications> poolNotifications = new Queue<MessNotifications>();

    public static void AddNotifications(MessNotifications notifi)
    {
        poolNotifications.Enqueue(notifi);
    }

    public static MessNotifications GetNextNotification()
    {
        if (poolNotifications.Count == 0)
        {
            return null;
        }
        return poolNotifications.Dequeue();
    }

    public static bool CheckNotifications() => poolNotifications.Count > 0;

}
