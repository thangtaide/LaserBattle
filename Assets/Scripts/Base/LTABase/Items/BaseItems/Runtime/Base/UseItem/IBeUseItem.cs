
using UnityEngine;
namespace LTA.Base.Item
{
    public interface IBeUseItem
    {
        void OnBeUseItem(Entity packItem,Transform ownItem);
    }
}
