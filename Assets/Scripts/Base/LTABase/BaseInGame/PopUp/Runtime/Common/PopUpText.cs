using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;
using LTA.Base;
using LTA.UI;
using TMPro;
namespace LTA.LTAPopUp
{
	public class PopUpText : BasePopUp
	{
		[SerializeField]
		protected TextMeshProUGUI txtTitle,txtMessage;
		
		//[SerializeField]
		//protected RectTransform rectTransform;

		public void Init(string message)
		{
			txtMessage.text = message;
			//StartCoroutine(ShowNormalMessage(message));
		}

		public void Init(string title,string message)
		{
			txtTitle.text = title;
			Init(message);
			//StartCoroutine(ShowNormalMessage(message));
		}

	}
}
