using LTA.Base.Item;
using UnityEngine;
using LTA.AutoTarget;
using LTA.Base;
using LTA.Base.Target;
public abstract class BaseUseItem : MonoBehaviour, IUseItem
{

    public abstract void UseItem();

    public UseItemController OwnUseItem
    {
        get
        {
            return ownUseItem;
        }
        set
        {
            ownUseItem = value;
        }
    }

    private UseItemController ownUseItem;
}
