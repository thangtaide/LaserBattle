using System;
using UnityEngine;
namespace LTA.Condition
{
    public abstract class BaseCondition : MonoBehaviour, ICondition
    {
        Action<ICondition> suitableCondition;

        public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
        [SerializeField]
        protected bool isSuitable = false;

        public virtual bool IsSuitable => isSuitable;

        protected void OnSuitableCondition(bool isSuitable)
        {
            //if (this.isSuitable == isSuitable) return;
            this.isSuitable = isSuitable;
            if (suitableCondition != null)
            {
                suitableCondition(this);
            }
        }
    }
}
