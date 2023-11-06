using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.SFS.Base;
using Sfs2X;
using Sfs2X.Core;
using System;
using LTA.LTALoading;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using LTA.DesignPattern;
public abstract class BaseRoomController : MonoBehaviour,IOnAddEvent
{
    Dictionary<string, BaseGame> dic_RoomName_BaseGame = new Dictionary<string, BaseGame>();

    public BaseGame GetBaseGame(string gameName)
    {
        if (dic_RoomName_BaseGame.ContainsKey(gameName)) return dic_RoomName_BaseGame[gameName];
        return null;
    }
    public Action<Room> _callbackJoinRoomSuccess;

    Action<Room> _callbackLeaveRoom;

    SmartFox sfs;

    public void OnAddEvent(SmartFox sfs)
    {
        this.sfs = sfs;
        sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
        sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnLeaveRoom);
    }

    public void SendRequestJoinRoom(string roomName,Action<Room> callbackJoinRoomSuccess)
    {
        _callbackJoinRoomSuccess = callbackJoinRoomSuccess;
        sfs.Send(new JoinRoomRequest(roomName));
    }

    public void SendRequestLeaveRoom(Room room, Action<Room> callbackLeaveRoom)
    {
        Loading.Instance.ShowNormalLoading();
        _callbackLeaveRoom = callbackLeaveRoom;
        sfs.Send(new LeaveRoomRequest(room));
    }

    public void SendRequest(string requestID, ISFSObject paramRequest,Room room, bool isShowLoading = true)
    {
        if (isShowLoading)
            Loading.Instance.ShowNormalLoading();
        sfs.Send(new ExtensionRequest(requestID, paramRequest,room));

    }

    void OnJoinRoom(BaseEvent evt)
    {
        Loading.Instance.ExitLoading();
        Room room = (Room)evt.Params["room"];
        BaseGame baseGame = CreateBaseGame();
        baseGame.Room = room;
        if (dic_RoomName_BaseGame.ContainsKey(room.Name))
            dic_RoomName_BaseGame.Remove(room.Name);

        dic_RoomName_BaseGame.Add(room.Name,baseGame);
        if (_callbackJoinRoomSuccess != null)
            _callbackJoinRoomSuccess(room);
    }

    //protected abstract void OnJoinRoom(Room room);

    void OnJoinRoomError(BaseEvent evt)
    {
        Loading.Instance.ExitLoading();
        string errorMsg = evt.Params["errorMessage"] as string;
        Debug.LogError(errorMsg);
    }
    protected abstract void OnJoinRoomError(string errorMsg);

    void OnLeaveRoom(BaseEvent sfsevent)
    {

        User user = (User)sfsevent.Params["user"];
        Room room = (Room)sfsevent.Params["room"];
        if (user == sfs.MySelf)
        {
            Loading.Instance.ExitLoading();
            
            if (dic_RoomName_BaseGame.ContainsKey(room.Name))
            {
                Destroy(dic_RoomName_BaseGame[room.Name]);
                dic_RoomName_BaseGame.Remove(room.Name);
            }
            if (_callbackLeaveRoom != null)
                _callbackLeaveRoom(room);
            OnLeaveRoom(room);
        }
    }

    protected abstract BaseGame CreateBaseGame();

    protected abstract void OnLeaveRoom(Room room);

}

public class BaseRoom : SingletonMonoBehaviour<BaseRoomController>
{

}