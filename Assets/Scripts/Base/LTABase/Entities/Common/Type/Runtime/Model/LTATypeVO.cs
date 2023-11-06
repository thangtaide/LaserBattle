using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class LTATypeVO : BaseMutilVO
{
    public LTATypeVO()
    {
        LoadData<BaseVO>("Entities", "typeInfo");
    }
}
