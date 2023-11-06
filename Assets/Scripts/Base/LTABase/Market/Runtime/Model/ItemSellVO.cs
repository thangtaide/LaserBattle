using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class ItemSellVO : BaseMutilVO
{
    public ItemSellVO()
    {
        LoadData<BaseVO>("Items","itemSellInfo");
    }
}
