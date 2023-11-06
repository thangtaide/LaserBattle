using LTA.DesignPattern;
using System;

namespace LTA.Base.Target
{
    public class TargetDataController : Singleton<TargetDataController>
    {
        public BaseTargetVO baseTargetVO;

        public virtual void LoadLocalData()
        {
            baseTargetVO = new BaseTargetVO();
        }
    }
}
