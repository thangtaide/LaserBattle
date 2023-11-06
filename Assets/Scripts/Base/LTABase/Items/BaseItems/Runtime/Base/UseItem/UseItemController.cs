using System.Collections.Generic;
using UnityEngine;
using LTA.Condition;
using LTA.VO;
using System;
using System.Reflection;

namespace LTA.Base.Item
{
    [System.Serializable]
    public class ItemInfo
    {
        public SubInfo[] conditions;
        public SubInfo[] useItemHandles;
    }
    [System.Serializable]


    public class UseItemController : MonoBehaviour
    {
        ItemInfo info;
        protected List<ICondition>      conditions      = new List<ICondition>();
        protected List<IResetCondition> resetConditions = new List<IResetCondition>();
        protected List<IUseItem>        useItems        = new List<IUseItem>();

        public Entity packItem;

        public List<ICondition> Conditions
        {
            get
            {
                 return conditions;
            }
        }

        public ItemInfo ItemInfo
        {
            set
            {
                Clear();
                info = value;
                if (value.conditions != null)
                {
                    UtilsVO.AddSubInfos(gameObject,value.conditions, (effectCondition) =>
                    {
                        ICondition condition = (ICondition)effectCondition;
                        condition.SuitableCondition = OnSuitableCondition;
                        
                        conditions.Add(condition);
                        if (effectCondition is IResetCondition)
                        {
                            resetConditions.Add((IResetCondition)effectCondition);
                        }
                    });
                }
                if (value.useItemHandles != null)
                {
                    UtilsVO.AddSubInfos(gameObject,value.useItemHandles, (effectUseItem) =>
                    {
                        IUseItem useItem = (IUseItem)effectUseItem;
                        useItem.OwnUseItem = this;
                        useItems.Add(useItem);
                    });
                }
                IAddItemInfo[] addItemInfos = GetComponentsInChildren<IAddItemInfo>();
                foreach (IAddItemInfo addItemInfo in addItemInfos)
                {
                    addItemInfo.OnAddItemInfo(info);
                }
            }
            get
            {
                return info;
            }
        }

        bool isUseingItem = false;

        void OnSuitableCondition(ICondition condition)
        {
            if (!IsAllowUseItem) return;
            UseItem();
        }

        public virtual void UseItem()
        {
            if (useItems == null) return;
            if (isUseingItem) return;
            isUseingItem = true;
            ResetCondition();
            foreach (IUseItem useItem in useItems)
            {
                useItem.UseItem();
            }

            IOnUseItem[] onUseItems = GetComponentsInChildren<IOnUseItem>();
            if (onUseItems == null)
            {
                Debug.Log(packItem.name);
            }
            foreach(IOnUseItem onUseItem in onUseItems)
            {
                onUseItem.OnUseItem(packItem);
            }
            isUseingItem = false;
        }

        public void ResetCondition()
        {
            foreach (IResetCondition resetCondition in resetConditions)
            {
                resetCondition.ResetCondition();
            }
        }

        public bool IsAllowUseItem
        {
            get
            {
                foreach (ICondition condition in conditions)
                {
                    if (!condition.IsSuitable)
                        return false;
                }
                return true;
            }
        }

        void Clear()
        {
            foreach (ICondition condition in conditions)
            {
                condition.SuitableCondition = null;
                Destroy((MonoBehaviour)condition);
            }
            conditions.Clear();
            resetConditions.Clear();
            foreach (IUseItem useItem in useItems)
            {
                MonoBehaviour monoUseItem = (MonoBehaviour)useItem;
                IOnRemoveUseItem onRemoveUseItem = monoUseItem.GetComponent<IOnRemoveUseItem>();
                if (onRemoveUseItem != null) onRemoveUseItem.OnRemoveItem();
                Destroy(monoUseItem);
            }
            useItems.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}
