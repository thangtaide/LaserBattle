using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LTA.Base;
using System;
using TMPro;
namespace LTA.LTALoading
{
    [DisallowMultipleComponent]
    public class LoadingProcessController : ProcessController
    {
        [SerializeField]
        RectTransform rect;

        [SerializeField]
        TextMeshProUGUI txtProcessing;

        [SerializeField]
        float timeDelayEndLoad;

        public Action EndLoading = null;

        public float CurrentPercent
        {
            get
            {
                return currentValue;
            }
        }

        protected override void OnUpdate(float value)
        {
            if(txtProcessing != null)
                txtProcessing.text = "Loading : " + (int)(((float)value/maxValue)*100) + "%";
            rect.sizeDelta = new Vector2(value, rect.sizeDelta.y);
            if (value == maxValue)
            {
                if (EndLoading != null)
                {
                    
                    EndLoading();
                    EndLoading = null;
                }
                //gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            maxValue = rect.sizeDelta.x;
            SetValue(0);
        }

        public void Reset()
        {
            this.gameObject.SetActive(true);
            SetValue(0);
        }

        public void SetPercent(float percent)
        {
            EditValue(percent * maxValue);
        }

        private void OnDisable()
        {
            SetValue(0);
        }
    }
}
