using UnityEngine;
using System;

namespace  LTA.Base
{
    public abstract class ProcessController : BehaviourController
    {
        [SerializeField]
        protected float maxValue;
        [SerializeField]
        protected float currentValue;

        protected void EditValue(float value,Action OnCompleteProcessing = null){
            try
            {
                float previousValue = currentValue;
                currentValue = value;
                if (currentValue <= 0)
                {
                    currentValue = 0;
                };
                if (currentValue > maxValue) currentValue = maxValue;
                UpdateValue(previousValue, currentValue, OnUpdate, OnCompleteProcessing);
            }
            catch (Exception e)
            {
                Debug.LogError("Error EditValue Processing " + e.Message);
            }
        }

        protected void SetValue(float value)
        {
            try
            {
                
                currentValue = value;
                if (currentValue <= 0)
                {
                    currentValue = 0;
                };
                if (currentValue > maxValue) currentValue = maxValue;
                OnUpdate(currentValue);
            }
            catch (Exception e)
            {
                Debug.LogError("Error SetValue Processing " + e.Message);
            }
        }

        protected void AddValue(float value)
        {
            try
            {

                currentValue = value;
                if (currentValue <= 0)
                {
                    currentValue = 0;
                };
                if (currentValue > maxValue) currentValue = maxValue;
            }
            catch (Exception e)
            {
                Debug.LogError("Error SetValue Processing " + e.Message);
            }
        }

        protected abstract void OnUpdate(float value);
    }
}
