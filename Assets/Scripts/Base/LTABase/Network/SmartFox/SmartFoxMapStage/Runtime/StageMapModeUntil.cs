using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using UnityEngine;

public class StageMapModeUntil : ListUtils<MapStageData>
{
    
    public static MapStageData GetStageById(string listName,string mapId)
    {
        List<MapStageData> packItems = GetListObj(listName);
        if (packItems == null) return null;
        foreach(MapStageData packItem in packItems)
        {
            if (packItem.mapId == mapId)
            {
                return packItem;
            }
        }
        return null;
    }
    
}
