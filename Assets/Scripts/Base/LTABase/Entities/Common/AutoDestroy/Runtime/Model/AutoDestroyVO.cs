using UnityEngine;
using LTA.VO;
using System;

public class AutoDestroyVO : BaseMutilVO
{
    public AutoDestroyVO()
    {
        LoadData<BaseVO>("Entities", "autoDestroyInfo");
    }
}
