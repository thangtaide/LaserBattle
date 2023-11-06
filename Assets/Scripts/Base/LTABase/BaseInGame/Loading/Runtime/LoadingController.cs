using UnityEngine;
using System;
using LTA.DesignPattern;

namespace LTA.LTALoading
{
    [DisallowMultipleComponent]
    public class LoadingController : MonoBehaviour
    {
        
        public LoadingProcessController loadingProcessController;

        [SerializeField]
        GameObject normalLoading;


        void Awake()
        {
            //DontDestroyOnLoad(gameObject);
            //ShowNormalLoading();
            //ExitLoading();
        }

        public void ShowLoadingProcess(float percent,Action EndLoading = null)
        {
            
            if (EndLoading != null)
            {
                loadingProcessController.Reset();
                loadingProcessController.EndLoading = EndLoading;
                loadingProcessController.gameObject.SetActive(true);
            }
            loadingProcessController.SetPercent(percent);
        }

        public void ExitLoading()
        {
            loadingProcessController.EndLoading = null;
            loadingProcessController.gameObject.SetActive(false);
            normalLoading.SetActive(false);
        }
        public void ShowNormalLoading()
        {
            normalLoading.SetActive(true);
        }
    }
    public class Loading : SingletonMonoBehaviour<LoadingController>
    {

    }
}

