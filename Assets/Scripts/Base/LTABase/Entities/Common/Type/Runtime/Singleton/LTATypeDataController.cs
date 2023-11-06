using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;

public class LTATypeDataController : Singleton<LTATypeDataController>
{
    public LTATypeVO typeVO;
    public void LoadLocalData()
    {
        typeVO = new LTATypeVO();
    }


}
