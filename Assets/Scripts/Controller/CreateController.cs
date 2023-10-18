using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.DesignPattern;

public class ITEMNAME
{
    public const string HP_ITEM = "Hp_Item";
    public const string GOLD_ITEM = "Gold_Item";
    public const string POWER_UP_ITEM = "Power_Up_Item";
}
public class CreateController : MonoBehaviour
{
    [SerializeField] Vector3 rangeRandomMax;
    [SerializeField] Vector3 rangeRandomMin;

    private Camera cameraMain;
    private float cameraHalfHeigh;
    private float cameraHalfWidth;
    [SerializeField] EnemyController prefabEnemy;
    [SerializeField] BossController prefabBoss;
    [SerializeField] RocketController prefabRocket;
    [SerializeField] ItemController hpItem;
    [SerializeField] ItemController goldItem;
    [SerializeField] ItemController powerUpItem;
    [SerializeField] Explosion explosion;
    [SerializeField] Explosion explosionRocket;
    [SerializeField] Explosion explosionSpace;
    private void Start()
    {
        cameraMain = Camera.main;
        cameraHalfHeigh = cameraMain.orthographicSize;
        cameraHalfWidth = cameraHalfHeigh * cameraMain.aspect;
    }
    public ItemController CreateItem(Vector3 position, string name)
    {
        ItemController item = null;
        if (name == ITEMNAME.HP_ITEM)
        {
            item = Instantiate(hpItem, position, hpItem.transform.rotation);
        }
        else if (name == ITEMNAME.GOLD_ITEM)
        {
            item = Instantiate(goldItem, position, goldItem.transform.rotation);
        }
        else if (name == ITEMNAME.POWER_UP_ITEM)
        {
            item = Instantiate(powerUpItem, position, powerUpItem.transform.rotation);
        }
        return item;
    }
    
    public BulletController CreateBullet(Vector3 position, Vector3 transformUp, BulletController prefabBullet)
    {
        BulletController bullet = Instantiate(prefabBullet, position, Quaternion.identity);
        bullet.transform.up = transformUp;
        return bullet;
    }
    public RocketController CreateRocket(Transform tranShoot, bool leftRight)
    {
        RocketController bullet = Instantiate(prefabRocket, tranShoot.position, Quaternion.identity);
        if (leftRight) { bullet.transform.Rotate(0, 0, 60); }
        else bullet.transform.Rotate(0, 0, -60);
        return bullet;
    }
    public Explosion CreateExplosion(Transform bullet)
    {
        return Instantiate(explosion, bullet.position, Quaternion.identity);
    }
    public Explosion CreateExplosionRocket(Transform bullet)
    {
        return Instantiate(explosionRocket, bullet.position, Quaternion.identity);
    }
    public Explosion CreateExplosionSpace(Transform space)
    {
        return Instantiate(explosionSpace, space.position, Quaternion.identity);
    }
    public void CreateEnemy(int level)
    {
        EnemyController enemy = Instantiate(prefabEnemy,  GetRanPos(), prefabEnemy.transform.rotation);
        enemy.lvController.Level = level;
    }
    public void CreateBoss(int level)
    {
        Vector3 position = new Vector3(0, rangeRandomMax.y, 0);
        BossController bossController = Instantiate(prefabBoss, position, prefabBoss.transform.rotation);
        bossController.lvController.Level = level;
    }
    Vector3 GetRanPos()
    {
        float x = Random.Range(rangeRandomMin.x + cameraHalfWidth, rangeRandomMax.x - cameraHalfWidth);
        if (x < 0) { x -= cameraHalfWidth; }
        else { x += cameraHalfWidth; }
        float y = Random.Range(cameraHalfHeigh, rangeRandomMax.y);
        return new Vector3(x, y, 0);
    }
}
public class Create : SingletonMonoBehaviour<CreateController>
{

}
