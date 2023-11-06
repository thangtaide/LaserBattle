using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string playerId;
    public string playerName;

    public PlayerData(ISFSObject sfsObject)
    {
        this.playerId = sfsObject.GetUtfString(GameRoomKey.PLAYER_ID);
        this.playerName = sfsObject.GetUtfString(GameRoomKey.PLAYER_NAME);
    }
}
