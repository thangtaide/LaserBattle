using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class SpinesVO : BaseMutilVO
{
    public SpinesVO()
    {
        LoadData<BaseVO>("Entities", "spineInfo");
    }
}
