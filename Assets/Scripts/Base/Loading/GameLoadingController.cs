using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.LTALoading;
using LTA.LTAScene;
using UnityEngine.SceneManagement;
using Base.DesignPattern;

public class GameLoadingController : LoadingController, ISceneManager
{
    public static string screenName = DefautSceneName.Menu;
    public void CloseScene(string sceneName)
    {
        SceneManager.UnloadScene(sceneName);
    }

    public void OpenScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }
    public void LoadScene()
    {
        ShowLoadingProcess(0.2f, () =>
        {
            SceneController.OpenScene(screenName);
        });
        ShowLoadingProcess(1f);
    }
    void Start()
    {
        GlobalScene.SceneManager = this;
        LoadScene();
    }
    /*private void Update()
    {
        if (SceneManager.GetActiveScene().name == screenName)
        {
            screenName = null;
            Debug.Log("done");
            ExitLoading();
        }
    }*/
}
public class GameLoading: Singleton<GameLoadingController> { }

