using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using LTA.Base.Item;
using System;

[System.Serializable]
public class UseItemsInfo
{
    public Entity[] items;
}

public class UseItemsVO : BaseMutilVO
{
    public UseItemsVO()
    {
        LoadData<BaseVO>("Entities", "useItems");
    }
}
