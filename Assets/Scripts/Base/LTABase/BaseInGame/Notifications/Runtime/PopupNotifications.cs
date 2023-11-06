using System.Collections;
using System.Collections.Generic;
using LTA.LTAPopUp;
using LTA.UI;
using UnityEngine;

public class PopupNotifications : PopUpText
{
    protected override void Awake()
    {
        BtnExit.OnClick(OnClickCloseHandler);    
    }

    private void OnClickCloseHandler(ButtonController callBack)
    {
        MessNotifications mess = NotifiUntils.GetNextNotification();
        if (mess != null)
        {
            Init(mess.title, mess.content);
        }
        else
        {
            ClosePopUp();    
        }
            
    }

    
}
