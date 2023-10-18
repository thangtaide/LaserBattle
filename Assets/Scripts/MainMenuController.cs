using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject panelUI;

    public void OnUpgrade()
    {
        panelUI.SetActive(true);
    }

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
