using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownInfo
{
    public float timeCountDown;
}

public class CountDownCondition : MonoBehaviour, ICondition
{
    CountDownInfo info;
    float countDown;
    public object Info
    {
        set
        {
            info = (CountDownInfo)value;
            countDown = info.timeCountDown;
        }
    }

    bool isSuitable = false;

    public bool IsSuitable => isSuitable;

    Action<ICondition> onSuitable;

    public Action<ICondition> OnSuitable { set => onSuitable = value; }

    public void ResetCondition()
    {
        isSuitable = false;
        countDown = info.timeCountDown;
    }

    private void Update()
    {
        if (countDown <= 0) return;
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            isSuitable = true;
            onSuitable(this);
        }
    }
}
