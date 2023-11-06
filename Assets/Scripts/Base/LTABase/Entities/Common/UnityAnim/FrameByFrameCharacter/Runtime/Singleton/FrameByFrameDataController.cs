using System;
using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using UnityEngine;

public class FrameByFrameDataController : Singleton<FrameByFrameDataController>
{
    public FrameByFrameVO frameByFrameVO;

    public void LoadLocalData()
    {
        frameByFrameVO = new FrameByFrameVO();
    }
}
