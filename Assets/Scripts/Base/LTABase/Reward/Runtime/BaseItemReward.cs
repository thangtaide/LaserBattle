using System;
using System.Collections;
using System.Collections.Generic;
using LTA.Base.Item;
using TMPro;
using UnityEngine;

namespace lta.smart_fox_reward
{
    public class BaseItemReward
    {
        [SerializeField] protected TextMeshProUGUI txtName;

        protected Action onClickOpenItemHandler;
        
        //public override void OnUpLevel(int level)
        //{
        //    base.OnUpLevel(level);
        //    if (this.txtName != null) txtName.text = name;
        //}

        protected virtual void OnClickOpenItemHandler()
        {
            if(onClickOpenItemHandler!= null) 
                onClickOpenItemHandler();
        }
    }    
}


