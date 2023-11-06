using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.LTALoading;
using System;
using SimpleJSON;
using System.IO;
using UnityEngine.Networking;
using LTA.LTAPopUp;

[System.Serializable]
public class BundleData
{
    public int version;
    public string fileName;

    public BundleData(string fileName, int version)
    {
        this.fileName = fileName;
        this.version = version;
    }

    public JSONObject ToJson()
    {
        JSONObject json = new JSONObject();
        json.Add("fileName", fileName);
        json.Add("version", version);
        return json;
    }
}
public abstract class AssetBundleLoading : LoadingController
{
    Action endCheckVersion;

    protected abstract string URL { get;}

    public void CheckVersion(Action endCheckVersion)
    {
        this.endCheckVersion = endCheckVersion;
        countFileDown = 0;
        Debug.Log("Test 1");
        //URL = URL + Application.version + "/";
        StartCoroutine(IeCheckVersion());
    }

    int countFileDown = 0;

    int sumFileDown = 1;

    IEnumerator OnCheckVersion(string data)
    {
        JSONArray array = JSON.Parse(data)["data"].AsArray;
        sumFileDown = array.Count;
        for (int i = 0; i < array.Count; i++)
        {
            BundleData bundleData = JsonUtility.FromJson<BundleData>(array[i].ToString());
            string savePath = Path.Combine(Application.persistentDataPath, bundleData.fileName);
            int currentVersion = PlayerPrefs.GetInt(bundleData.fileName, 0);
            if (System.IO.File.Exists(savePath) && currentVersion >= bundleData.version)
            {
                countFileDown++;
                Debug.Log(countFileDown);
                Loading.Instance.ShowLoadingProcess(0.05f * countFileDown);
                continue;
            }
            bool isDone = false;
            yield return IeLoadData(bundleData,()=>{
                isDone = true;
            });
            yield return new WaitUntil(()=> isDone);
        }

    }

    IEnumerator IeCheckVersion()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL + "version.json");
        www.timeout = 5;
        www.SendWebRequest();
        yield return new WaitUntil(()=> www.isDone);

            if (www.error != null)
            {

                int isLoadAsset = PlayerPrefs.GetInt("isLoadAsset", 0);
                if (isLoadAsset == 1) endCheckVersion();
                else
                {
                    yield return new WaitForEndOfFrame();
                    ShowError(www.error);
                }
            }
            else
            {

                StartCoroutine(OnCheckVersion(www.downloadHandler.text));
                yield return new WaitUntil(() => countFileDown == sumFileDown);
                PlayerPrefs.SetInt("isLoadAsset", 1);
                endCheckVersion();
            }


    }

    IEnumerator IeLoadData(BundleData bundleData,Action endLoadData)
    {
        Debug.Log(URL + bundleData.fileName);
        using (UnityWebRequest www = UnityWebRequest.Get(URL + bundleData.fileName))
        {
            DownloadHandler handle = www.downloadHandler;
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                ShowError(www.error);
            }
            else
            {
                string savePath = Path.Combine(Application.persistentDataPath, bundleData.fileName);
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                System.IO.File.WriteAllBytes(savePath, www.downloadHandler.data);
                PlayerPrefs.SetInt(bundleData.fileName, bundleData.version);
                countFileDown++;
                Loading.Instance.ShowLoadingProcess(0.05f * countFileDown);
            }
        }
        if (endLoadData != null)
        {
            endLoadData();
        }
    }

    void ShowError(string error)
    {
        PlayerPrefs.SetInt("isLoadAsset", 0);
        PopUpText _PopUpError = PopUp.Instance.ShowLocalPopUp<PopUpText>("PopUpText");
        _PopUpError.SetClosePopUp(() => {
            PopUp.Instance.CloseAllPopUp();
            CheckVersion(endCheckVersion);
        });
        _PopUpError.Init("Network Error",error);
    }
}
