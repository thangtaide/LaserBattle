using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
public class HPVO : BaseMutilVO
{
    public HPVO()
    {
        LoadData<BaseVO>("Entities", "hpInfo");
    }
}
