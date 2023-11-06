using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class SpaceInfo
{
    public int damage;
    public int hp;
}
public abstract class SpaceController : MoveController, IHit
{
    [SerializeField] protected BulletController prefabBullet;
    public HpController hpController;
    public LevelController lvController;
    [SerializeField] protected float timeToNextShoot = 1.5f;
    public bool canTakeDame = true;
    HideHP hideHP;
    public float currentDmg;
    protected float spawnShoot;
    public float TimeToNextShoot { get { return timeToNextShoot; } set { timeToNextShoot = value; } }
    protected virtual void Awake()
    {
        hpController.onDie = OnDie;
        lvController.OnLevelUp = OnLevelUp;
        lvController.CurrentValue = 0;
        hideHP = GetComponentInChildren<HideHP>();
    }
    protected virtual void Start()
    {
        OnLevelUp(lvController.Level);
        spawnShoot = Time.time;
    }
    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
    }
    protected virtual void Shoot()
    {
        if (Time.time - spawnShoot > timeToNextShoot && prefabBullet != null)
        {
            spawnShoot = Time.time;
            BulletController clone = Create.Instance.CreateBullet(transform.position,transform.up, prefabBullet);
            clone.DmgBullet = currentDmg;
            SoundController.instance.PlaySound("sfx_laser1");
        }

    }

    /*public void OnHit(BulletController bulletController)
    {
        if (canTakeDame)
        {
            if (hideHP != null)
            {
                hideHP.ShowAlpha();
            }
            hpController.TakeDamage(bulletController.DmgBullet);
        }
    }*/
    public void OnHit(float dmgTake)
    {
        if (canTakeDame)
        {
            if (hideHP != null)
            {
                hideHP.ShowAlpha();
            }
            hpController.TakeDamage(dmgTake);
        }
    }
    protected abstract void OnDie();
    protected virtual void OnLevelUp(int level)
    {
        SpaceInfo spaceInfo = GetSpaceInfor(level);
        hpController.HP = spaceInfo.hp;
        currentDmg = spaceInfo.damage;

    }
    protected abstract SpaceInfo GetSpaceInfor(int level);
}
