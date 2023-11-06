using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject closePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        closePanel.SetActive(false);
    }
}
