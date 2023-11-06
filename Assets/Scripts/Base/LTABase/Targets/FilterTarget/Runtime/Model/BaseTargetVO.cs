using LTA.VO;
using System;
using UnityEngine;
namespace LTA.Base.Target
{
    public class BaseTargetVO : BaseMutilVO
    {
        public BaseTargetVO()
        {
            LoadData<BaseDetailTargetVO>("Targets", "targetInfo");
        }

        public TargetInfo GetTargetInfo(string targetName, int level)
        {
            return ((BaseDetailTargetVO)dic_Data[targetName]).GetTargetDetailInfo(level);
        }
    }
}
