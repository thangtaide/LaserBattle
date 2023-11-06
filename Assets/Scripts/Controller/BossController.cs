using Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [SerializeField] BulletController[] prefabBossBulletNotRotate;
    [SerializeField] BulletController[] prefabBossBulletRotate;
    [SerializeField] float TimeBossShoot;
    int turn = 1;
    int randBullet;
    float currentTime;
    protected override SpaceInfo GetSpaceInfor(int level)
    {
        return DataManager.Instance.bossVO.GetSpaceInfo(level);
    }
    protected override void Start()
    {
        base.Start();
        currentTime = Time.time;
        GetRanBullet();
    }
    protected void Shoot(BulletController bullet, float rotate)
    {
            for (int i = 0; i < 6; i++)
            {
                BulletController clone = Create.Instance.CreateBullet(transform.position,transform.up, bullet);
                clone.gameObject.transform.Rotate(0, 0, 60 * i + rotate);
                clone.DmgBullet = currentDmg;
            }
        SoundController.instance.PlaySound("laser_shot");
    }
    void Update()
    {
        if (Time.time - currentTime >= TimeBossShoot && turn <3)
        {
            turn++;
            currentTime = Time.time;
            for(int i = 0; i < 15; i++)
            {
                float rotate;
                BulletController clone;
                if(randBullet < prefabBossBulletNotRotate.Length)
                {
                    clone = prefabBossBulletNotRotate[randBullet];
                    rotate = 0;
                }
                else
                {
                    clone = prefabBossBulletRotate[randBullet - prefabBossBulletNotRotate.Length];
                    rotate = i * 10;
                }
                StartCoroutine(ExampleCoroutine(i*0.15f,clone,rotate));
            }
        }else if(turn == 3)
        {
            turn = 1;
            GetRanBullet();
        }
        while (Vector3.Distance(randPos, transform.position) < 1 || Vector3.Distance(randPos, transform.position) > 10)
        {
            GetRandomPosition();
        }

        Move(randPos - transform.position);
    }
    void GetRanBullet()
    {
        randBullet = Random.Range(0,prefabBossBulletNotRotate.Length+ prefabBossBulletRotate.Length);
    }
    private IEnumerator ExampleCoroutine(float time, BulletController bullet, float rotate)
    {
        yield return new WaitForSeconds(time);
        Shoot(bullet, rotate);
    }
    protected override void OnDie()
    {
        ObServer.Instance.Notify(TOPICNAME.BOSS_DIE, this);
        Explosion explosion =  Create.Instance.CreateExplosionSpace(transform);
        explosion.transform.localScale = new Vector3(2, 2, 0);
        SoundController.instance.PlaySound("ExplosionEffect");
        Destroy(gameObject);
    }
}


