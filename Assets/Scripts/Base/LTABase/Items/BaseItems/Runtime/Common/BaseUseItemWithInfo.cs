using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Base.Item
{
    public abstract  class BaseUseItemWithInfo<T> : BaseUseItem,ISetInfo
    {
        [SerializeField]
        protected T info;

        public virtual object Info
        {
            set
            {
                info = (T)value;
            }
        }

        public T ItemInfo
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
            }
        }
    }
}
