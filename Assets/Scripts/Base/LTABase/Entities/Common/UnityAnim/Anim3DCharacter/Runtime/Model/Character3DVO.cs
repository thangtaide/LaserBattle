using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class Character3DVO : BaseMutilVO
{
    public Character3DVO()
    {
        LoadData<BaseVO>("Entities", "character3DInfo");
    }
}
