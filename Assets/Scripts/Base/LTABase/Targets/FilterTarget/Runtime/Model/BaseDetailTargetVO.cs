using LTA.VO;
using SimpleJSON;
namespace LTA.Base.Target
{
    public class BaseDetailTargetVO : BaseVO
    {
        public TargetInfo GetTargetDetailInfo(int level)
        {
            TargetInfo itemInfo = GetData<TargetInfo>(level);
            if (itemInfo == null) return null;
            JSONObject json = GetData(level);
            AddTargetDetailInfo(itemInfo, json);
            return itemInfo;
        }

        protected virtual void AddTargetDetailInfo(TargetInfo targetInfo, JSONObject json)
        {
            if (json["bestTargets"] != null)
                targetInfo.bestTargets = UtilsVO.GetSubInfos(json["bestTargets"].AsArray);
            if (json["targets"] != null)
                targetInfo.targets = UtilsVO.GetSubInfos(json["targets"].AsArray);
        }
    }
}
