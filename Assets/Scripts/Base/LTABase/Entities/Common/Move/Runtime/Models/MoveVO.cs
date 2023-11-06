using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class MoveVO : BaseMutilVO
{
    public MoveVO()
    {
        LoadData<BaseVO>("Entities", "moveInfo");
    }
}
