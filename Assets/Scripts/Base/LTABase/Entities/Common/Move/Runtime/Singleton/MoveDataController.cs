using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;

public class MoveDataController : Singleton<MoveDataController>
{
    public MoveVO moveVO;
    public void LoadDataLocal()
    {
        moveVO = new MoveVO();
    }
}
