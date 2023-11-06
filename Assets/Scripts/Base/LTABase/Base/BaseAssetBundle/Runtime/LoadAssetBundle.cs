using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using LTA.LTAPopUp;
using LTA.LTAScene;
using LTA.LTALoading;

public class LoadAssetBundle
{
    //static Action onLoadAssetBundleFail;

    //public static Action OnLoadAssetBundleFail
    //{
    //    set
    //    {
    //        onLoadAssetBundleFail = value;
    //    }
    //}
    static AssetBundle assetBundleResources;

    static Dictionary<string, AssetBundle> dic_DataName_AssetBunlde = new Dictionary<string, AssetBundle>();

    public static T LoadAsset<T>(string dataName) where T : Object
    {
        if (assetBundleResources == null)
            assetBundleResources = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "resources"));
        if (assetBundleResources == null)
        {
            ShowError();
            return null;
        }
        return assetBundleResources.LoadAsset<T>(dataName);
    }

    public static AssetBundleRequest LoadAssetAsync<T>(string dataName) where T : Object
    {
        if (assetBundleResources == null)
            assetBundleResources = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "resources"));
        if (assetBundleResources == null)
        {
            ShowError();
            return null;
        }
        return assetBundleResources.LoadAssetAsync<T>(dataName);
    }

    public static T[] LoadAssets<T>(string dataName) where T : Object
    {
        if (!dic_DataName_AssetBunlde.ContainsKey(dataName))
        {
            dic_DataName_AssetBunlde.Add(dataName, AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, dataName)));
        }
        AssetBundle dataLoadedAssetBundle = dic_DataName_AssetBunlde[dataName];
        if (dataLoadedAssetBundle == null)
        {
            ShowError();
            return null;
        }
        return dataLoadedAssetBundle.LoadAllAssets<T>();
    }

    public static IEnumerator LoadAllAssetGame(Action endLoad)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(Path.Combine(Application.persistentDataPath, "resources"));
        Loading.Instance.loadingProcessController.timePerforme = 8f;
        Loading.Instance.ShowLoadingProcess(0.90f);
        yield return new WaitUntil (() =>request.isDone);
        Loading.Instance.loadingProcessController.timePerforme = 1f;
        Loading.Instance.ShowLoadingProcess(0.91f);
        assetBundleResources = request.assetBundle;
        if (assetBundleResources == null)
        {
            ShowError();
        }
        else
        {
            AssetBundleRequest request1 = assetBundleResources.LoadAllAssetsAsync();
            yield return new WaitUntil(() => request1.isDone);
            Loading.Instance.ShowLoadingProcess(0.92f);
            request = AssetBundle.LoadFromFileAsync(Path.Combine(Application.persistentDataPath, "scene"));
            yield return new WaitUntil(() => request.isDone);
            Loading.Instance.ShowLoadingProcess(0.95f);
            AssetBundle assetBundle = request.assetBundle;
            string[] scenePaths = assetBundle.GetAllScenePaths();
            foreach (string path in scenePaths)
            {
                Debug.Log(path);
            }
            if (endLoad != null)
            {
                endLoad();
            }
        }
    }



    static void ShowError()
    {
            PlayerPrefs.SetInt("isLoadAsset", 0);
            PopUpText _PopUpError = PopUp.Instance.ShowPopUp<PopUpText>("PopUpText");
            _PopUpError.SetClosePopUp(()=>
            {
                SceneController.OpenScene("Loading");
            });
            _PopUpError.Init("Error", "Failed to load AssetBundle!");
    }
}
