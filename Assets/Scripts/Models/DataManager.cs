using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.DesignPattern;

public class DataManager : Singleton<DataManager>
{
    public PlayerVO playerVO;

    public EnemyVO enemyVO;

    public BossVO bossVO;

    public void LoadData()
    {
        playerVO = new PlayerVO();

        enemyVO = new EnemyVO();

        bossVO = new BossVO();
    }
}
