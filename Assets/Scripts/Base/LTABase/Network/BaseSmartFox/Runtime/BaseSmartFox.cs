using LTA.LTALoading;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Util;
using Sfs2X.Protocol.Serialization;
using UnityEngine;
using System;
using System.Collections;
using LTA.LTAPopUp;
using System.Collections.Generic;
using LTA.DesignPattern;
using UnityEngine.Networking;

namespace LTA.SFS.Base
{
    public abstract class BaseSmartFox : MonoBehaviour
    {
        
        protected SmartFox sfs;

        public SmartFox CurrentSFS
        {
            get
            {
                return sfs;
            }
        }

        protected abstract SmartFox SFS { get; }

        protected abstract string IP { get; }

        protected abstract int Port { get; }

        protected abstract string Zone { get; }

        protected int pingTime;

        public int PingTime
        {
            get => pingTime;
            set
            {
                pingTime = value;
                Observer.Instance.Notify("Ping", pingTime);
            }
        }

        Action _callBackConnectSuccess;

        private Action _callBackConnectFalse;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void ConnectedToServer(Action onConnectSuccess , Action onConnectFalse)
        {
            Loading.Instance.ShowNormalLoading();
            _callBackConnectSuccess = onConnectSuccess;
            _callBackConnectFalse = onConnectFalse;
            sfs = SFS;
            AddEvent();
            ConfigData cfg = new ConfigData();
            cfg.Host = IP;
            cfg.Port = Port;
            cfg.Zone = Zone;
            Debug.Log("IP " + cfg.Host + "Port " + cfg.Port + " " + cfg.Zone);
            sfs.Connect(cfg);
            
        }

        protected void OnCryptoInit(BaseEvent sfsevent)
        {
            Debug.Log("OnCryptoInit");
            Dictionary<string, object> Params = (Dictionary<string,object>)sfsevent.Params;
            foreach (KeyValuePair<string,object> param in Params)
            {
                Debug.Log(param.Key);
            }
            Debug.Log(sfsevent.Params["success"]);
            Debug.Log(sfsevent.Params["errorMessage"]);
            Debug.Log("IP " + sfs.CurrentIp + " " + sfs.CurrentPort + " " + sfs.CurrentZone);
            if (_callBackConnectSuccess != null)
            {
                _callBackConnectSuccess();
            }
        }

        protected virtual void AddEvent()
        {
            sfs.AddEventListener(SFSEvent.CRYPTO_INIT, OnCryptoInit);
            sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnResponse);
            sfs.AddEventListener(SFSEvent.PING_PONG, OnPingPong);
            IOnAddEvent[] onAddEvents = GetComponents<IOnAddEvent>();
            foreach(IOnAddEvent onAddEvent in onAddEvents)
            {
                onAddEvent.OnAddEvent(sfs);
            }
        }
        
        void OnPingPong(BaseEvent sfsevent)
        {
            PingTime = (int)sfsevent.Params["lagValue"];
        }
        
        protected virtual void OnConnection(BaseEvent sfsevent)
        {
            Dictionary<string, object> Params = (Dictionary<string, object>)sfsevent.Params;
            foreach (KeyValuePair<string, object> param in Params)
            {
                Debug.Log(param.Key);
            }
            Debug.Log(sfsevent.Params["success"]);
            Debug.Log("IP " + sfs.CurrentIp + "port " + sfs.CurrentPort + "zone " + sfs.CurrentZone);
            if (sfs.CurrentPort == -1) _callBackConnectFalse();
            //sfs.EnableLagMonitor(true);
/*#if !UNITY_WEBGL || !UNITY_STANDALONE_WIN
            sfs.InitCrypto();
#else
            if (_callBackConnectSuccess != null)
            {
                _callBackConnectSuccess();
                StartCoroutine(CheckInternet());
            }
#endif*/
            if (_callBackConnectSuccess != null)
            {
                _callBackConnectSuccess();
                StartCoroutine(CheckInternet());
            }
            Loading.Instance.ExitLoading();
            
        }

        void FixedUpdate()
        {
#if !UNITY_WEBGL
            if (sfs != null)
            {
                sfs.ProcessEvents();
                //SendPingRequest();
            }
            if (Application.internetReachability == NetworkReachability.NotReachable && sfs != null )
            {
                Debug.Log("Kill connect");
                sfs.KillConnection();
                sfs.Disconnect();
                
            }   
#else
            if (sfs != null)
            {
                sfs.ProcessEvents();
            }
#endif


        }
        void SendPingRequest()
        {
            sfs.Send(new ExtensionRequest("Ping",new SFSObject()));
        }
        protected virtual void OnConnectionLost(BaseEvent sfsevent)
        {
            Debug.Log("OnConnectionLost");
            Loading.Instance.ExitLoading();
            sfs = null;
        }

        public void SendRequest(string requestID, ISFSObject paramRequest,bool isShowLoading = true)
        {
            if (isShowLoading)
                Loading.Instance.ShowNormalLoading();
            sfs.Send(new ExtensionRequest(requestID, paramRequest));

        }

        void OnResponse(BaseEvent sfsevent)
        {
            ISFSObject data = (ISFSObject)sfsevent.Params["params"];
            string cmd = (string)sfsevent.Params["cmd"];
            OnResponse(cmd, data);
        }
        
        Dictionary<string,IOnResponse> dic_Cmd_Response = new Dictionary<string, IOnResponse>();
        protected virtual void OnResponse(string cmd, ISFSObject data)
        {
            Debug.Log("respone : __"+ cmd);
            Loading.Instance.ExitLoading();
            if (cmd == ErrorKey.ERROR)
            {
                if(!data.ContainsKey(ErrorKey.MESSAGE)) return;
                string mess = data.GetUtfString(ErrorKey.MESSAGE);
                PopUpText _PopUpError = PopUp.Instance.ShowPopUp<PopUpText>(PopUpSFSName.PopUpText);
                _PopUpError.Init("Error", mess);
                return;
            }else if (cmd == "Ping")  
            {

            }
            if (dic_Cmd_Response.ContainsKey(cmd))
                dic_Cmd_Response[cmd].OnResponse(data);
        }
        public void AddResponse(string cmd,IOnResponse onResponse) 
        {
            if (!dic_Cmd_Response.ContainsKey(cmd))
            {
                dic_Cmd_Response.Add(cmd,onResponse);
            }
        }
        public void RemoveResponse(string cmd)
        {
            if (dic_Cmd_Response.ContainsKey(cmd))
            {
                dic_Cmd_Response.Remove(cmd);
            }
        }

        private IEnumerator CheckInternet()
        {
            const string echoServer = "https://star.shibamons.com:8443/okwar/heroes/report";
            bool result;
            using (var request = UnityWebRequest.Head(echoServer))
            {
                request.timeout = 5;
                yield return request.SendWebRequest();
                result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
            }
           // Debug.Log("result : " + result);
            if (result)
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(CheckInternet());
            }
            else
            {
                Debug.Log("request false");
                sfs.KillConnection();
                sfs.Disconnect();
            }
        }
    }

}
