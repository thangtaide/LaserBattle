using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class FrameByFrameVO : BaseMutilVO
{
    public FrameByFrameVO()
    {
        LoadData<BaseVO>("Entities", "frameByFrameInfo");
    }
}
