using System.Collections;
using System.Collections.Generic;
using LTA.SFS.Base;
using Sfs2X.Entities.Data;
using UnityEngine;

public class MapStageResponse : IOnResponse
{
    
    private string listEntitiesName;

    public MapStageResponse(string listEntitiesName)
    {
        this.listEntitiesName = listEntitiesName;
    }

    public void OnResponse(ISFSObject data)
    {
        if (ListUtils<MapStageData>.dic_Name_ListObj.ContainsKey(listEntitiesName))
        {
            ListUtils<MapStageData>.dic_Name_ListObj[listEntitiesName].Clear();
        }
       
        ISFSArray rewardItems = data.GetSFSArray("Entities");
      
        
        for(int i = 0;i < rewardItems.Count;i++)
        {
            ISFSObject item = rewardItems.GetSFSObject(i);
            
            AddEntity(new MapStageData{ star =  item.GetInt("star"), mapId = item.GetUtfString("mapId"),modeId = item.GetUtfString("modeId"),type = item.GetUtfString("type")});
            
        }
        
        
    }
    
    private void AddEntity(MapStageData mapStage )
    {
        StageMapModeUntil.AddObjRespond(listEntitiesName, mapStage);
    }
}
