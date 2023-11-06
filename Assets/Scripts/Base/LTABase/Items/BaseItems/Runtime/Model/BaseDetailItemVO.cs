using LTA.VO;
using SimpleJSON;
namespace LTA.Base.Item
{
    public class BaseDetailItemVO : BaseVO
    {
        public ItemInfo GetItemDetailInfo(int level)
        {
            ItemInfo itemInfo = GetData<ItemInfo>(level);
            if (itemInfo == null) return null;
            JSONObject json = GetData(level);
            AddItemDetailInfo(itemInfo, json);
            return itemInfo;
        }

        protected virtual void AddItemDetailInfo(ItemInfo itemInfo, JSONObject json)
        {
            if (json["conditions"] != null)
                itemInfo.conditions = UtilsVO.GetSubInfos(json["conditions"].AsArray);
            if (json["useItemHandles"] != null)
                itemInfo.useItemHandles = UtilsVO.GetSubInfos(json["useItemHandles"].AsArray);
        }
    }
}
