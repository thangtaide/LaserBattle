using LTA.LTALoading;
using LTA.SFS.Base;
using Sfs2X;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGame : MonoBehaviour,IOnResponse
{
    public Dictionary<string, Queue<ISFSObject>> dic_gameID_queueData = new Dictionary<string, Queue<ISFSObject>>();
    public Dictionary<string, GameRoom> dic_gameID_gameRoom = new Dictionary<string,GameRoom>();

    public GameRoom GetGameRoom(string gameId)
    {
        if (dic_gameID_gameRoom.ContainsKey(gameId))
            return dic_gameID_gameRoom[gameId];
        return null;
    }

    Room room;

    public Room Room
    {
        set
        {
            room = value;
            BaseSmartFox.AddResponse(room.Name, this);
        }
        get
        {
            return room;
        }
    }

    BaseSmartFox baseSmartFox;

    BaseSmartFox BaseSmartFox
    {
        get
        {
            if (baseSmartFox == null)
                baseSmartFox = GetComponent<BaseSmartFox>();
            return baseSmartFox;
        }
    }

    public void OnResponse(ISFSObject data)
    {
        string gameId = data.GetUtfString(GameRoomKey.GAME_ID);
        Debug.Log("GameRoom " + gameId);
        if (!dic_gameID_queueData.ContainsKey(gameId))
        {
            dic_gameID_queueData.Add(gameId, new Queue<ISFSObject>());
            GameRoom gameRoom = CreateGameRoom(gameId);
            gameRoom.gameName = room.Name;
            gameRoom.gameId = gameId;
            dic_gameID_gameRoom.Add(gameId, gameRoom);
        }
        dic_gameID_queueData[gameId].Enqueue(data);
        
    }

    protected abstract GameRoom CreateGameRoom(string gameId);

    public void JoinGame(ISFSObject paramRequest)
    {
        Debug.Log("JoinGame");
        JoinGame("",paramRequest);
    }

    public void JoinGame(string gameId,ISFSObject paramRequest)
    {
        paramRequest.PutUtfString(GameRoomKey.GAME_ID,gameId);
        SendRequest(GameRoomCmd.JOIN_GAME,paramRequest,true);
    }

    public void LeaveGame(string gameId)
    {
        ISFSObject paramRequest = new SFSObject();
        paramRequest.PutUtfString(GameRoomKey.GAME_ID, gameId);
        SendRequest(GameRoomCmd.LEAVE_GAME, paramRequest, true);
    }

    public void PlayerAction(string gameId,ISFSObject paramRequest)
    {
        paramRequest.PutUtfString(GameRoomKey.GAME_ID, gameId);
        SendRequest(GameRoomCmd.PLAYER_ACTION, paramRequest);
    }

    public void SendRequest(string requestID, ISFSObject paramRequest = null, bool isShowLoading = false)
    {
        if (isShowLoading)
            Loading.Instance.ShowNormalLoading();
        BaseRoom.Instance.SendRequest(requestID,paramRequest,room,isShowLoading);

    }

    private void OnDestroy()
    {
        BaseSmartFox.RemoveResponse(room.Name);
        foreach (KeyValuePair<string,GameRoom> keyValuePair in dic_gameID_gameRoom)
        {
            Destroy(keyValuePair.Value);
        }
    }
}
