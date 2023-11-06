using System.Collections;
using System.Collections.Generic;
using lta.smart_fox_reward;
using UnityEngine;

public class ItemRewardPvP : ItemReward
{

    public int level;
    public string nickName;
    public string mapId;
    public bool isLock;
    public int season;
    
    public ItemRewardPvP(string item, string type, int amount , int level , string nickName, string mapId , bool isLock, int season) : base( item, type, amount)
    {
        this.level = level;
        this.mapId = mapId;
        this.nickName = nickName;
        this.isLock = isLock;
        this.season = season;
    }
}
