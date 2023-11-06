using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.LTAPopUp;
using UnityEngine.UI;

namespace  lta.smart_fox_reward
{
    public abstract class PopUpRewards : BasePopUp
    {
        [SerializeField] protected GameObject rewardItem;
        
        [SerializeField] protected RectTransform contentRewards;
        public virtual void OnShow(List<ItemReward> itemRewards)
        {
            for (int i = 0; i < itemRewards.Count; i++)
            {
                //BaseItemReward item = AddItemReward(rewardItem, contentRewards);
                //item.name = itemRewards[i].item;
                //item.OnUpLevel(1);
                
            }
        }

        #region createItem
        
        //private BaseItemReward AddItemReward(GameObject gameObject,RectTransform contentRewards)
        //{
        //    GameObject item = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        //    item.transform.SetParent(contentRewards);
        //    item.transform.localScale = Vector3.one;
        //    item.AddComponent<BaseItemReward>();
        //    return item.GetComponent<BaseItemReward>();
        //}
        

        #endregion


    }    
}
