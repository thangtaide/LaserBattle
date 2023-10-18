using Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInfo
{
    public int gold;
}

public class GoldController : MonoBehaviour, ItemEffect
{
    public GoldInfo info;

    public object Info { set => info = (GoldInfo)value; }

    public void Active()
    {
        Player.Instance.currenMoney += info.gold;
        ObServer.Instance.Notify(TOPICNAME.EARNED_GOLD);
        SoundController.instance.PlaySound("Coin_PickUp");
        Destroy(this);

    }
}
