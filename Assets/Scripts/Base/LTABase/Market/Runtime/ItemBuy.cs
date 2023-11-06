using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace LTA.Market
{
    [System.Serializable]
    public class ItemBuyInfo
    {
        public float price;
    }

    public class ItemBuy : ItemShop, IPointerClickHandler
    {
        float price = 0;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (price == 0) return;
            if (MarketVal.coin < price) return;
            MarketUtils.UpdateCoin(-price);
            //PackItem packItem = new PackItem(name, GetComponent<NonEntityController>().Level);
            //ListItemUtils.AddObjRespond(MarketVal.listItemsName, packItem);
        }

        //public override void OnUpLevel(int level)
        //{
        //    base.OnUpLevel(level);
        //    if (!MarketDataController.Instance.itemBuyVO.checkKey(name)) return;
        //    ItemBuyInfo itemBuyInfo = MarketDataController.Instance.itemBuyVO.GetData<ItemBuyInfo>(name,level);
        //    if (itemBuyInfo == null) return;
        //    price = itemBuyInfo.price;
        //}
    }
}
