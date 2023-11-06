using System.Collections.Generic;
using LTA.DesignPattern;
using LTA.SFS.Base;
using Sfs2X.Entities.Data;
using UnityEngine;

public class SmartFoxRank : IOnResponse
{
    public string listRankName;

    public SmartFoxRank(string PvPRank)
    {
        this.listRankName = PvPRank;
    }

    public virtual void OnResponse(ISFSObject data)
    {
        //ListUtils<PackItemExp>.dic_nameListObjRespond.Clear();
         ISFSArray ranks = data.GetSFSArray("Entities");
        for(int i = 0;i < ranks.Count;i++)
        {
            
            ISFSObject rank = ranks.GetSFSObject(i);
            
            AddRank(rank.GetUtfString("EntityName"), rank.GetInt("level"),rank.GetUtfString("nickname"),rank.GetUtfString("mapId"),rank.GetInt("exp"),rank.GetUtfString("type"));
        }
        //Observer.Instance.Notify(KeyCodeRank.RESPONSE_PVP_RANKING,ListUtils<PackItemExp>.dic_nameListObjRespond[listRankName]);

    }

    public void AddRank(string entityName, int level,string nickname,string mapId,int exp,string type)
    {
        Debug.Log(("AddRank " + nickname));
        //ListUtils<PackItemExp>.AddObjRespond(listRankName, new PackItemExp(nickname, exp,entityName,level))
    }

}



