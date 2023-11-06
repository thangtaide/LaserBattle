
using UnityEngine;
using UnityEngine.UI;
using LTA.UI;
using System;
using LTA.Base;
using TMPro;
namespace LTA.LTAPopUp
{
    public class PopUpYesNo : PopUpText
    {

        [SerializeField]
        private ButtonController BtnYes;

        public void SetPopUp(Action CallBackYes = null,Action CallBackNo = null)
        {
            SetPopUp("Yes","No",CallBackYes,CallBackNo);
        }

        public void SetPopUp(string YesText, string NoText, Action CallBackYes = null,Action CallBackNo = null)
        {
            BtnYes.GetComponentInChildren<TextMeshProUGUI>().text = YesText;
            BtnExit.GetComponentInChildren<TextMeshProUGUI>().text = NoText;
            BtnYes.OnClick((btn) =>
            {
                
                ClosePopUp(CallBackYes);
            });
            if (CallBackNo == null) return;
            Debug.Log("Set Click No");
            BtnExit.OnClick((btn)=>         
            {
                ClosePopUp(CallBackNo);
            });
        }
    }
}
