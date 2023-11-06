using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using LTA.LTALoading;
using LTA.SFS.Base;
using Sfs2X.Entities.Data;
using UnityEngine;

namespace lta.smart_fox_reward
{
    public class RewardNormalResponse : IOnResponse
    {
        public void OnResponse(ISFSObject data)
        {
            Debug.Log("data reward ");
            ISFSArray rewardItems = data.GetSFSArray("Entities");
            List<ItemReward> lstItemReward = new List<ItemReward>();
        
            for(int i = 0;i < rewardItems.Count;i++)
            {
                ISFSObject item = rewardItems.GetSFSObject(i);
                lstItemReward.Add(new ItemReward(item.GetUtfString("item"),item.GetUtfString("type"),item.GetInt("amount")));
            
            }
        
            Observer.Instance.Notify(KeyReward.KEY_COMMAND, lstItemReward);
            Debug.LogWarning("data reward "+ rewardItems);
        }
    }

}