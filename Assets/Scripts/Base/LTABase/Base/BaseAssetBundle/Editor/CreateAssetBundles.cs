

using System.IO;
using UnityEditor;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;



public class CreateAssetBundles
{
    [MenuItem("LTA/Build AssetBundles/Android")]
    static void BuildAllAssetBundlesAndroid()
    {
        string assetBundleDirectory = "AssetBundles/Android/"+ Application.version+"/";
        string jsonText = CreateVersionFile(assetBundleDirectory);
        if (Directory.Exists(assetBundleDirectory))
        {
            Directory.Delete(assetBundleDirectory, true);
        }
        Directory.CreateDirectory(assetBundleDirectory);
        AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.Android);
        UpdateVersionFile(assetBundleManifest, assetBundleDirectory, jsonText);
    }

    [MenuItem("LTA/Build AssetBundles/Windows")]
    static void BuildAllAssetBundlesWindows()
    {
        
        string assetBundleDirectory = "AssetBundles/Windows/"+ Application.version + "/";
        string jsonText = CreateVersionFile(assetBundleDirectory);
        if (Directory.Exists(assetBundleDirectory))
        {
            Directory.Delete(assetBundleDirectory, true);
        }
        Directory.CreateDirectory(assetBundleDirectory);
        AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneWindows);
        UpdateVersionFile(assetBundleManifest, assetBundleDirectory, jsonText);

    }

    [MenuItem("LTA/Build AssetBundles/MacOS")]
    static void BuildAllAssetBundlesMacOS()
    {
        string assetBundleDirectory = "AssetBundles/MacOS/" +Application.version + "/";
        string jsonText = CreateVersionFile(assetBundleDirectory);
        if (Directory.Exists(assetBundleDirectory))
        {
            Directory.Delete(assetBundleDirectory, true);
        }
        Directory.CreateDirectory(assetBundleDirectory);
        AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneOSX);
        UpdateVersionFile(assetBundleManifest, assetBundleDirectory, jsonText);
    }

    [MenuItem("LTA/Build AssetBundles/IOS")]
    static void BuildAllAssetBundlesIOS()
    {
        
        string assetBundleDirectory = "AssetBundles/IOS/" + Application.version + "/";
        string jsonText = CreateVersionFile(assetBundleDirectory);
        if (Directory.Exists(assetBundleDirectory))
        {
            Directory.Delete(assetBundleDirectory, true);
        }
        Directory.CreateDirectory(assetBundleDirectory);
        AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.iOS);
        UpdateVersionFile(assetBundleManifest, assetBundleDirectory, jsonText);
    }

    [MenuItem("LTA/ClearCache")]
    static void ClearCache()
    {
        Caching.ClearCache();
    }

    static string CreateVersionFile(string assetBundleDirectory)
    {
        string path = Path.Combine(assetBundleDirectory, "version.json");

        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        return null;
    }
   
    static void UpdateVersionFile(AssetBundleManifest assetBundleManifest, string assetBundleDirectory,string jsonText)
    {
        string path = Path.Combine(assetBundleDirectory, "version.json");
        List<string> assetBundles = new List<string>(assetBundleManifest.GetAllAssetBundles());
        JSONArray array;
        
        if (jsonText != null)
        {
            array = JSON.Parse(jsonText)["data"].AsArray;
            for (int i = 0; i < array.Count; i++)
            {
                BundleData bundleData = JsonUtility.FromJson<BundleData>(array[i].ToString());
                bundleData.version++;
                array[i] = bundleData.ToJson();
                assetBundles.Remove(bundleData.fileName);
            }
        }
        else
        {
            array = new JSONArray();
        }
        foreach(string assetBundle in assetBundles)
        {
            BundleData bundleData = new BundleData(assetBundle,1);
            array.Add(bundleData.ToJson());
        }
        JSONObject data = new JSONObject();
        data.Add("data",array);
        Debug.Log(data);
        File.WriteAllText(path,data.ToString());
    }
}
