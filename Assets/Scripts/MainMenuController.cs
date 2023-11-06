using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LTA.LTAScene;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject upgradePanelUI;
    public GameObject changeSpcaeUI;
    public TextMeshProUGUI currentGoldTxt;
    public float delay = 1f;
    int totalGold, currentGold;
    private void Awake()
    {
        //GameController.TotalMoney = 7900;
        totalGold = GameController.TotalMoney;
        currentGoldTxt.text = "Gold: " + totalGold;
    }
    private void Update()
    {
        currentGoldTxt.text = "Gold: " +totalGold;
        if(totalGold != GameController.TotalMoney)
        {
            if (Mathf.Abs(GameController.TotalMoney - totalGold) <= 100)
            {
                totalGold = GameController.TotalMoney;
            }else
            totalGold -= 75;
        }
    }
    public void OnChangeSpcae()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        changeSpcaeUI.SetActive(true);
    }
    public void OnUpgrade()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        upgradePanelUI.SetActive(true);
    }

    public void OnStart()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        GameLoadingController.screenName = DefautSceneName.GamePlay;
        SceneManager.LoadScene("Loading");
    }
    public void LoadMainMenu()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        GameLoadingController.screenName = DefautSceneName.Menu;
        SceneManager.LoadScene("Loading");
    }
    public void OnExit()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        Application.Quit();
    }
}
