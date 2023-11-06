using Sfs2X.Entities.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRoom : MonoBehaviour
{
    public string gameName;

    public string gameId;

    bool isProcessing = false;

    protected string currentStateGame = GameState.BEGIN_GAME;

    protected List<Player> players = new List<Player>();
    protected abstract string MyPlayerId { get; }
    private void Update()
    {
        if (isProcessing) return;
        isProcessing = true;
        Dictionary<string, Queue<ISFSObject>> dic_gameID_queueData = BaseRoom.Instance.GetBaseGame(gameName).dic_gameID_queueData;
        if (!dic_gameID_queueData.ContainsKey(gameId)) return;
        if (BaseRoom.Instance.GetBaseGame(gameName).dic_gameID_queueData[gameId].Count == 0)
        {
            isProcessing = false;
            return;
        }
        ISFSObject data = BaseRoom.Instance.GetBaseGame(gameName).dic_gameID_queueData[gameId].Dequeue();
        string cmd = data.GetUtfString(GameRoomKey.CMD);
        string gameState = data.GetUtfString(GameRoomKey.GAME_STATE);
        currentStateGame = gameState;
        Debug.Log("GameRoom " + cmd);
        switch (cmd)
        {
            case GameRoomCmd.JOIN_GAME:
                OnJoinGame(data.GetSFSArray(GameRoomKey.LIST_PLAYERS));
                break;
            case GameRoomCmd.NEW_USER_JOIN_GAME:
                OnNewUserJoinGame(data.GetSFSObject(GameRoomKey.NEW_PLAYER_DATA));
                break;
            case GameRoomCmd.LEAVE_GAME:
                OnLeaveGame(data.GetUtfString(GameRoomKey.PLAYER_ID));
                break;
            case GameRoomCmd.CHANGE_GAME_STATE:
                OnChangeGameState(data);
                break;
            case GameRoomCmd.PLAYER_ACTION:
                OnPlayerAction(data);
                break;

        }
        isProcessing = false;
    }

    void OnPlayerAction(ISFSObject sfsObject)
    {
       
        string playerId = sfsObject.GetUtfString(GameRoomKey.PLAYER_ID);
        Debug.Log("OnPlayerAction " + playerId);
        Player player = GetPlayer(playerId);
        player.controller.queueAction.Enqueue(sfsObject);
    }

    protected virtual void OnJoinGame(ISFSArray listPlayers)
    {
        for(int i = 0; i < listPlayers.Count;i++)
        {
            ISFSObject sfsPlayer = listPlayers.GetSFSObject(i);
            OnNewUserJoinGame(sfsPlayer);
        }
    }

    void OnNewUserJoinGame(ISFSObject sfsPlayer)
    {
        Player player = CreatePlayer(sfsPlayer);
        player.controller = CreatePlayerController(player.playerData);
        players.Add(player);
    }

    protected abstract PlayerController CreatePlayerController(PlayerData playerData);

    protected virtual Player CreatePlayer(ISFSObject sfsPlayer)
    {
        return new Player(sfsPlayer,new PlayerData(sfsPlayer.GetSFSObject(GameRoomKey.PLAYER_DATA)));
    }

    void OnLeaveGame(string playerId)
    {
        if (playerId == MyPlayerId)
        {
            OnMyLeaveGame();
            return;
        }
        try
        {
            Player player = GetPlayer(playerId);
            players.Remove(player);
            Destroy(player.controller.gameObject);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }    

    protected virtual void OnMyLeaveGame()
    {
        Destroy(this);
    }

    public Player GetPlayer(string playerId) 
    {
        foreach(Player player in players)
        {
            if (player.playerData.playerId == playerId)
                return player;
        }
        throw new GameErrorException(this);
    }

    public Player GetMyPlayer()
    {
        return GetPlayer(MyPlayerId);
    }

    public void LeaveGame()
    {
        GetComponent<BaseGame>().LeaveGame(gameId);
    }

    protected abstract void OnChangeGameState(ISFSObject data);
    private void OnDestroy()
    {
        foreach (Player player in players)
        {
            Destroy(player.controller.gameObject);
        }
    }
}

