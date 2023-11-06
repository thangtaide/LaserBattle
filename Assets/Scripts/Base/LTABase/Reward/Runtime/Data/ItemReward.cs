using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lta.smart_fox_reward
{
    public class ItemReward
    {
        public string item;
        public string type;
        public int amount;

        public ItemReward(string item, string type, int amount)
        {
            this.item = item;
            this.type = type;
            this.amount = amount;
        }
    }
}

