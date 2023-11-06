using System;
using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using UnityEngine;

public class Character3DDataController : Singleton<Character3DDataController>
{
    public Character3DVO character3DVO;

    public void loadLocalData()
    {
        character3DVO = new Character3DVO();
    }
}
