using LTA.DesignPattern;
using System;

namespace LTA.Base.Item
{
    public class ItemDataController : Singleton<ItemDataController>
    {
        public BaseItemVO baseItemVO;

        public UseItemsVO useItemsVO;

        public virtual void LoadLocalData()
        {
            baseItemVO = new BaseItemVO();

            useItemsVO = new UseItemsVO();
        }
    }
}
