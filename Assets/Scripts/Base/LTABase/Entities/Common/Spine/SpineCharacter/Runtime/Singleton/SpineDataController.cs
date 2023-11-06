using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;

public class SpineDataController : Singleton<SpineDataController>
{
    public SpinesVO spinesVO;

    public void LoadLocalData()
    {
        spinesVO = new SpinesVO();
    }
}
