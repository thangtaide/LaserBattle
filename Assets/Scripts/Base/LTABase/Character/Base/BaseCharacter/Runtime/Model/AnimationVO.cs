using LTA.VO;
using System;

public class AnimationVO : BaseMutilVO
{
    public AnimationVO()
    {
        LoadData<BaseVO>("Entities","animationInfo");
    }
}
