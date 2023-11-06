using Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    void OnHit(float dmg);
}
public class BulletController : MonoBehaviour
{
    [SerializeField] float leftTime = 2f;
    [SerializeField] float dmgBullet = 20f;
    private float spawnTime;

    public float DmgBullet { get { return dmgBullet; } set { if(value>0) dmgBullet = value; } }
    
    private void OnEnable()
    {
        spawnTime = Time.time;
    }
    protected virtual void Update()
    {
        if (Time.time - spawnTime > leftTime)
        {
            DestroyBullet();
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHit iHit = collision.gameObject.GetComponent<IHit>();
        if (iHit != null)
        {
                iHit.OnHit(dmgBullet);
        }
        DestroyBullet();
    }
    public virtual void DestroyBullet()
    {
        Create.Instance.CreateExplosion(transform);
        Destroy(gameObject);
    }
   
}
