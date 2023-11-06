using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerData playerData;
    public bool isPlaying = false;
    public bool isReady = true;
    public PlayerController controller;
    public Player(ISFSObject sfsObject,PlayerData playerData)
    {
        isPlaying = sfsObject.GetBool(GameRoomKey.IS_PLAYING);
        isReady = sfsObject.GetBool(GameRoomKey.IS_READY);
        this.playerData = playerData;
    }
    
}
