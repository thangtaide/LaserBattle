using UnityEngine;
using LTA.Base;
using System;

public class Damage
{
    public float damage;
    public Transform ownDamage;
    public Damage(float damage,Transform transform)
    {
        this.damage = damage;
        ownDamage = transform;
    }
}
[System.Serializable]
public class HPInfo
{
    public int hp;
}

public class HPController : ProcessController,IOnUpLevel
{
    public float CurrentHP
    {
        get
        {
            return currentValue;
        }
        set
        {
            SetValue(value);
        }
    }

    public float HP
    {
        get
        {
            return maxValue;
        }
        set
        {
            maxValue = HP;
            AddValue(maxValue);
        }
    }

    public void EditHP(float HP, Action OnCompleteProcessing = null)
    {
        maxValue = HP;
        EditValue(maxValue, OnCompleteProcessing);
    }

    public void TakeDamage(Damage damage)
    {
        IBeforeTakeDamge[] beforeTakeDamges = transform.parent.GetComponentsInChildren<IBeforeTakeDamge>();
        foreach(IBeforeTakeDamge beforeTakeDamge in beforeTakeDamges)
        {
            beforeTakeDamge.OnBeforeTakeDamge(damage);
        }
        EditValue(currentValue - damage.damage);
        IAfterTakeDamage[] afterTakeDamages = transform.parent.GetComponentsInChildren<IAfterTakeDamage>();
        foreach (IAfterTakeDamage afterTakeDamage in afterTakeDamages)
        {
            afterTakeDamage.OnAfterTakeDamage(damage);
        }
    }

    public void ResetHP()
    {
        SetValue(maxValue);
    }

    [SerializeField]
    bool isDie = false;

    protected override void OnUpdate(float value)
    {
        if (value == 0)
        {
            if (isDie) return;
            isDie = true;
            IOnDie[] onDies = transform.parent.GetComponentsInChildren<IOnDie>();
            foreach(IOnDie onDie in onDies)
            {
                onDie.OnDie();
            }
        }
    }

    public void OnUpLevel(int level)
    {
        if (!HPDataController.Instance.hpVO.CheckKey(transform.parent.name)) return;
        HPInfo hpInfo = HPDataController.Instance.hpVO.GetData<HPInfo>(transform.parent.name, level);
        if (hpInfo == null) return;
        isDie = false;
        HP = hpInfo.hp;
    }
}