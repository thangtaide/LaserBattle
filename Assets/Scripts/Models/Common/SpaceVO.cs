using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class SpaceVO : BaseVO
{
    public SpaceInfo GetSpaceInfo(int level)
    {
        JSONArray array = data.AsArray;
        if (level >= array.Count) return JsonUtility.FromJson<SpaceInfo>(array[array.Count - 1].ToString());
        return JsonUtility.FromJson<SpaceInfo>(array[level - 1].ToString());
    }
}
