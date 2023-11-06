using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;

public class MarketDataController : Singleton<MarketDataController>
{
    public ItemSellVO itemSellVO;

    public ItemBuyVO itemBuyVO;
    public void LoadDataLocal()
    {
        itemBuyVO = new ItemBuyVO();
        itemSellVO = new ItemSellVO();
    }
}
