using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using LTA.SFS.Base;
using Sfs2X.Entities.Data;
using UnityEngine; 
using lta.notify;

public class SmartFoxNotificationsResponse : IOnResponse
{
    public void OnResponse(ISFSObject data)
    {
        
        NotifiUntils.AddNotifications(new MessNotifications(){title = data.GetUtfString("Title"), content = data.GetUtfString("Message")});
        Observer.Instance.Notify( lta.notify.KeyCode.KEY_CODE_NOTIFICATION);
    }
}
