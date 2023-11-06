using lta.data;
using Sfs2X.Core;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;
using LTA.SFS.Base;
using LTA.DesignPattern;
using System;

namespace LTA.SFS.Login
{
    public abstract class BaseSmartFoxLogin : BaseSmartFox
    {
        public bool IsConnected
        {
            get { return sfs != null; }
        }

        Action _callbackLoginSuccess;

        Action _callbackLoginFail;

        public void Login(LoginData loginData, Action onLoginSuccess,Action onLoginFail)
        {
            if (IsConnected)
            {
                SFSObject parameters = new SFSObject();
                parameters.PutClass("DataLogin", loginData);
                sfs.Send(new LoginRequest(loginData.username, loginData.password, Zone, parameters));
            }
            else
            {
                ConnectedToServer(()=>
                {
                    SFSObject parameters = new SFSObject();
                    parameters.PutClass("DataLogin", loginData);
                    sfs.Send(new LoginRequest(loginData.username, loginData.password, Zone, parameters));
                },OnConnectFail);    
            }
            _callbackLoginSuccess = onLoginSuccess;
            _callbackLoginFail = onLoginFail;
        }

        protected override void AddEvent()
        {
            base.AddEvent();
            sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            sfs.AddEventListener(SFSEvent.LOGOUT, OnLogOut);
        }


        protected virtual void OnLogin(BaseEvent sfsevent)
        {
            Debug.Log("OnLogin");
            if (_callbackLoginSuccess != null)
                _callbackLoginSuccess();
        }

        protected virtual void OnLogOut(BaseEvent sfsevent)
        {
            sfs = null;
        }

        protected virtual void OnLoginError(BaseEvent sfsevent)
        {
            Debug.Log("OnLoginError");
            if (_callbackLoginFail != null)
                _callbackLoginFail();
        }
        protected abstract void OnConnectFail();
        protected override void OnResponse(string cmd, ISFSObject data)
        {
            if (cmd == UserKey.UPDATE_USER_INFORMATION)
            {
                UserGlobal.userData = new UserData();
                UserGlobal.userData.userId = data.GetUtfString(UserKey.USER_ID);
                UserGlobal.userData.totalDiamond = data.GetInt(UserKey.TOTAL_DIAMOND);
                UserGlobal.userData.totalMoney = data.GetInt(UserKey.TOTAL_MONEY);
                UserGlobal.userData.reconnect = data.GetInt(UserKey.RECONNECT);
                UserGlobal.userData.userName = data.GetUtfString(UserKey.USER_NAME);
                Observer.Instance.Notify(UserKey.UPDATE_USER_INFORMATION);
                return;
            }
            base.OnResponse(cmd, data);
        }
    }
}
