using Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHPInfo
{
    public int hp;
}

public class EffectHPController : MonoBehaviour, ItemEffect
{
    public EffectHPInfo info;

    public object Info { set => info = (EffectHPInfo)value; }

    public void Active()
    {
        HpController hpController = GetComponentInChildren<HpController>();
        hpController.CurrentValue += info.hp;
        SoundController.instance.PlaySound("power_up_sound");
        ObServer.Instance.Notify(TOPICNAME.INCREASE_HP);
        Destroy(this);
        
    }
}
