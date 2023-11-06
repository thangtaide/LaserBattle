using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;

public class AutoDestroyDataController : Singleton<AutoDestroyDataController>
{
    public AutoDestroyVO autoDestroyVO;
    public void LoadLocalData()
    {
        autoDestroyVO = new AutoDestroyVO();
    }
}
