using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItemController : MonoBehaviour, IHit
{
    [SerializeField] HpController hpController;
    HideHP hideHP;
    private void Awake()
    {
        hpController.onDie = OnOpen;
        hideHP = GetComponentInChildren<HideHP>();
    }

    public void OnHit(BulletController bulletController)
    {
        if (hideHP != null)
        {
            hideHP.ShowAlpha();
        }
        hpController.TakeDamage(bulletController.DmgBullet);
    }
    void OnOpen()
    {
        string itemName;
        int a = Random.Range(0, 10);
        if (a <= 3)
        {
            itemName = ITEMNAME.HP_ITEM;
        }
        else itemName = ITEMNAME.GOLD_ITEM;
        Create.Instance.CreateItem(transform.position, itemName);
        Destroy(gameObject);
    }
    
}
