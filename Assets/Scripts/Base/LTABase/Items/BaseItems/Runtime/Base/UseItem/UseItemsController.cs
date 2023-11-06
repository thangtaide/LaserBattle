using System.Collections.Generic;
using UnityEngine;

namespace LTA.Base.Item
{
    public class UseItemsController : MonoBehaviour, IOnUpLevel
    {
        Dictionary<string, UseItemController> dic_itemName_useItem = new Dictionary<string, UseItemController>();
        List<Entity> packItems = new List<Entity>();
        private IOnRemoveAllItem[] onRemoveAllItem;

        public int ItemCount
        {
            get
            {
                return packItems.Count;
            }
        }

        public Entity GetItem(string itemName)
        {
            foreach (Entity packItem in packItems)
            {
                if (packItem.name == itemName)
                {
                    return packItem;
                }
            }
            return null;
        }

        public Entity GetItem(Entity checkPackItem)
        {
            foreach (Entity packItem in packItems)
            {
                if (packItem == checkPackItem)
                {
                    return packItem;
                }
            }
            return null;
        }

        public IOnRemoveAllItem[] OnRemoveAllItem
        {
            get
            {
                if (onRemoveAllItem == null) onRemoveAllItem = GetComponents<IOnRemoveAllItem>();
                return onRemoveAllItem;
            }
        }

        public UseItemController AddItem(Entity item,int index)
        {
            if (!ItemDataController.Instance.baseItemVO.CheckKey(item.name)) return null;
            ItemInfo itemInfo = ItemDataController.Instance.baseItemVO.GetItemInfo(item.name, item.level);
            if (itemInfo == null) return null;
            if (index >= packItems.Count)
            {
                return AddItem(item);
            }
            Entity packItem = packItems[index];

            if (dic_itemName_useItem.ContainsKey(packItem.name))
            {
                UseItemController oldUseItem = dic_itemName_useItem[packItem.name];
                dic_itemName_useItem.Remove(packItem.name);
                Destroy(oldUseItem);
            }

            UseItemController useItem;
            if (!dic_itemName_useItem.ContainsKey(item.name))
            {
                useItem = gameObject.AddComponent<UseItemController>();
                dic_itemName_useItem.Add(item.name, useItem);
                packItems[index] = item;
            }
            else
            {
                useItem = dic_itemName_useItem[item.name];
                item = packItems[index];
            }
            useItem.packItem = item;
            useItem.ItemInfo = itemInfo;
            IAddItemsInfo[] addItemsInfos = GetComponentsInChildren<IAddItemsInfo>();
            foreach (IAddItemsInfo addItemsInfo in addItemsInfos)
            {
                addItemsInfo.OnAddItemsInfo(dic_itemName_useItem.Count, item, useItem);
            }
            return useItem;

        }

        public UseItemController AddItem(Entity item)
        {
            if (!ItemDataController.Instance.baseItemVO.CheckKey(item.name)) return null;
            ItemInfo itemInfo = ItemDataController.Instance.baseItemVO.GetItemInfo(item.name, item.level);
            if (itemInfo == null) return null;
            UseItemController useItem;
            if (!dic_itemName_useItem.ContainsKey(item.name))
            {
                useItem = gameObject.AddComponent<UseItemController>();
                dic_itemName_useItem.Add(item.name, useItem);
                packItems.Add(item);
            }
            else
            {
                useItem = dic_itemName_useItem[item.name];
            }
            useItem.packItem = item;
            useItem.ItemInfo = itemInfo;
            IAddItemsInfo[] addItemsInfos = GetComponentsInChildren<IAddItemsInfo>();
            foreach (IAddItemsInfo addItemsInfo in addItemsInfos)
            {
                addItemsInfo.OnAddItemsInfo(dic_itemName_useItem.Count, item, useItem);
            }
            return useItem;
            
        }

        public void RemoveItem(UseItemController useItem)
        {
            packItems.Remove(GetItem(useItem.packItem));
            dic_itemName_useItem.Remove(useItem.packItem.name);
            Destroy(useItem);
        }

        public void RemoveItem(string itemName)
        {
            if (!dic_itemName_useItem.ContainsKey(itemName)) return;
            packItems.Remove(GetItem(itemName));
            Destroy(dic_itemName_useItem[itemName]);
            dic_itemName_useItem.Remove(itemName);
        }

        public void RemoveItem()
        {
            if (OnRemoveAllItem != null)
            {
                foreach (IOnRemoveAllItem item in OnRemoveAllItem)
                {
                    item.OnRemoveAll();
                }
            }

            foreach (KeyValuePair<string, UseItemController> itemName_useItem in dic_itemName_useItem)
            {
                Destroy(itemName_useItem.Value);
            }
            packItems.Clear();
            dic_itemName_useItem.Clear();
        }

        //public void UseItem(string itemName, Transform[] targets)
        //{
        //    if (!dic_itemName_useItem.ContainsKey(itemName)) return;
        //    dic_itemName_useItem[itemName].UseItem(targets);
        //}

        //public void UseItem(Transform[] targets)
        //{
        //    foreach(KeyValuePair<string,UseItemController> itemName_useItem in dic_itemName_useItem)
        //    {
        //        itemName_useItem.Value.UseItem(targets);
        //    }
        //}

        public bool IsAllowUseItem(string itemName)
        {
            if (!dic_itemName_useItem.ContainsKey(itemName)) return false;
            return dic_itemName_useItem[itemName].IsAllowUseItem;
        }

        public void OnUpLevel(int level)
        {
            if (!ItemDataController.Instance.useItemsVO.CheckKey(name)) return;
            RemoveItem();
            UseItemsInfo useItemsInfo  = ItemDataController.Instance.useItemsVO.GetData<UseItemsInfo>(name, level);
            if (useItemsInfo == null) return;
            Entity[] items = useItemsInfo.items;
            for (int i = 0; i < items.Length;i++)
            {
                Entity item = items[i];
                UseItemController useItem = AddItem(item); 
                
            }
            IEndAddItemsInfo[] endItemsInfos = GetComponentsInChildren<IEndAddItemsInfo>();
            if (endItemsInfos == null) return;
            foreach (IEndAddItemsInfo endItemsInfo in endItemsInfos)
            {
                endItemsInfo.OnEndAddItems();
            }
        }
        

    }
}
