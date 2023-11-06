using UnityEngine;
using LTA.Base.Target;
using LTA.AutoTarget;
namespace LTA.Base.Item
{
    [System.Serializable]
    public class UseItemInfo
    {
        public int numTarget = 0;
        public Entity typeTarget = null;
    }
    public abstract class BaseUseItemEffect<T> : BaseUseItemWithInfo<T> where T : UseItemInfo
    {
        FilterTargetController filterTarget;

        FilterTargetController FilterTarget
        {
            get
            {
                if (ItemInfo.typeTarget == null) return null;
                FilterTargetsController filterTargets = GetComponentInChildren<FilterTargetsController>();
                if (filterTargets == null)
                    filterTargets = gameObject.AddComponent<FilterTargetsController>();
                if (filterTarget == null)
                    filterTarget = filterTargets.AddFilterTarget(ItemInfo.typeTarget,this);

                return filterTarget;
            }
        }

        public override void UseItem()
        {
            if (FilterTarget == null) UseItem(new Transform[0]);
            UseItem(TargetController.GetTargets(filterTarget, ItemInfo.numTarget).ToArray());
        }

        public virtual void UseItem(Transform[] targets)
        {
            foreach (Transform target in targets)
            {
                UseItem(target);
            }
        }
        public virtual void UseItem(Transform target)
        {
            IBeUseItem[] beUseItems = target.GetComponentsInChildren<IBeUseItem>();
            foreach (IBeUseItem beUseItem in beUseItems)
            {
                beUseItem.OnBeUseItem(OwnUseItem.packItem,transform);
            }
        }

        private void OnDestroy()
        {
            FilterTargetsController filterTargets = GetComponentInChildren<FilterTargetsController>();
            if (filterTargets == null) return;
                GetComponent<FilterTargetsController>().RemoveFilterTarget(ItemInfo.typeTarget, this);
        }
    }
}
