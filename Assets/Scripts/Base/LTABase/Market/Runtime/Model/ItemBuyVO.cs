using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class ItemBuyVO : BaseMutilVO
{
    public ItemBuyVO()
    {
        LoadData<BaseVO>("Items","itemBuyInfo");
    }
}
