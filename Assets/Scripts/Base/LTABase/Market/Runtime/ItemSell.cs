using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace LTA.Market
{
    [System.Serializable]
    public class ItemSellInfo
    {
        public float price;
    }

    public class ItemSell : ItemShop, IPointerClickHandler
    {
        public float price;
        public void OnPointerClick(PointerEventData eventData)
        {
            //PackItem packItem = ListItemUtils.GetItem(MarketVal.listItemsName, name);
            //if (packItem == null) return;
            //MarketUtils.UpdateCoin(price);
            //ListItemUtils.RemoveObject(MarketVal.listItemsName, packItem);
            
        }

        //public override void OnUpLevel(int level)
        //{
        //    base.OnUpLevel(level);
        //    if (!MarketDataController.Instance.itemBuyVO.checkKey(name)) return;
        //    ItemSellInfo itemSellInfo = MarketDataController.Instance.itemSellVO.GetData<ItemSellInfo>(name, level);
        //    if (itemSellInfo == null) return;
        //    price = itemSellInfo.price;
        //}
    }
}
