using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;

public class AvatarDataController : Singleton<AvatarDataController>
{
    Dictionary<string, AvatarVO> dic_Name_AvatarVO = new Dictionary<string, AvatarVO>();

    public AvatarVO GetAvatarVO(string dataName)
    {
        if (!dic_Name_AvatarVO.ContainsKey(dataName)) throw new Exception(dataName + " is null");
        return dic_Name_AvatarVO[dataName];
    }

    public void LoadLocalData(string dataName)
    {
        if (dic_Name_AvatarVO.ContainsKey(dataName)) return;
        dic_Name_AvatarVO.Add(dataName, new AvatarVO(dataName));
    }
}
