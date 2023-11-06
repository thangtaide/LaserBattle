using LTA.VO;
using System;

namespace LTA.Base.Item
{
    public class BaseItemVO : BaseMutilVO
    {
        public BaseItemVO()
        {
            LoadData<BaseDetailItemVO>("Items", "itemInfo");
        }

        public ItemInfo GetItemInfo(string skillName, int level)
        {
            return ((BaseDetailItemVO)dic_Data[skillName]).GetItemDetailInfo(level);
        }
    }
}
