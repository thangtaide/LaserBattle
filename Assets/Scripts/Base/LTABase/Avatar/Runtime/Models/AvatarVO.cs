using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.VO;
using System;

public class AvatarVO : BaseMutilVO
{
    public AvatarVO(string data)
    {
        LoadData<BaseVO>(data,"avatarInfo");
    }
}
