using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.DesignPattern;

public class EnemyController : SpaceController
{
    [SerializeField]protected LimitMove limitMove;
    [SerializeField] Vector3 limitShoot;
    bool damageToPlayer = false;
    protected Vector3 randPos;
    protected override void Start()
    {
        base.Start();
        GetRandomPosition();
    }
    
    void Update()
    {
        while (Vector3.Distance(randPos, transform.position) < 1 || Vector3.Distance(randPos, transform.position) > 10)
        {
            GetRandomPosition();
        }

        Move(randPos - transform.position);
        if (Player.Instance != null )
        {
            if (transform.position.x > -limitShoot.x && transform.position.x < limitShoot.x && transform.position.y > -limitShoot.y && transform.position.y < limitShoot.y)
            {
                Shoot();
            }
            if (damageToPlayer)
            {
                float damage = currentDmg * Time.deltaTime*10;
                Player.Instance.OnHit(damage);
            }
        }
        if(hpController.HP <= 0)
        {
            OnDie();
        }
    }
    protected void GetRandomPosition()
    {
        randPos =new Vector3(Random.Range(limitMove.minLimit.x, limitMove.maxLimit.x), Random.Range(limitMove.minLimit.y, limitMove.maxLimit.y));
    }

    protected override SpaceInfo GetSpaceInfor(int level)
    {
        return DataManager.Instance.enemyVO.GetSpaceInfo(level);
    }
    protected override void OnDie()
    {
        ObServer.Instance.Notify(TOPICNAME.ENEMY_DIE, this);
        Create.Instance.CreateExplosionSpace(transform);
        SoundController.instance.PlaySound("ExplosionEffect");
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHit iHit = collision.gameObject.GetComponent<PlayerController>();
        if (iHit != null)
        {
            damageToPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        IHit iHit = collision.gameObject.GetComponent<PlayerController>();
        if (iHit != null)
        {
            damageToPlayer = false;
        }
    }
}

