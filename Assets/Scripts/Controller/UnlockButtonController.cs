using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UnlockButtonController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int goldToUnlock;
    [SerializeField] TextMeshProUGUI notEnoughGold;
    [SerializeField] TextMeshProUGUI goldNeedToUnlock;
    [SerializeField] bool isActive = true;
    [SerializeField] bool useDefault = true;
    [SerializeField] bool useDefaultName = false;
    [SerializeField] string panelName;

    private void Awake()
    {
        if(useDefaultName) { panelName = transform.parent.gameObject.name; }
        goldNeedToUnlock.text = goldToUnlock + " G";
    }
    private void OnEnable()
    {
        notEnoughGold.gameObject.SetActive(false);
        if (GameController.GetInt(panelName) != 0 && !useDefault)
        {
            isActive = false;
        }
        if(isActive)
        {
            GameController.SetInt(panelName, 0);
            gameObject.SetActive(true);
        }
        else
        {
            GameController.SetInt(panelName, 1);
            gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameController.TotalMoney >= goldToUnlock)
        {
            SoundController.instance.PlaySound("CashEffect");
            GameController.TotalMoney -= goldToUnlock;
            this.gameObject.SetActive(false);
            GameController.SetInt(panelName, 1);
        }
        else
        {
            notEnoughGold.gameObject.SetActive(false);
            notEnoughGold.gameObject.SetActive(true);
        }
    }
}
