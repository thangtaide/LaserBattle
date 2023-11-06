
using System.IO;
using UnityEditor;
using UnityEngine;
using SimpleJSON;

#if (UNITY_EDITOR) 
public class RefreshJson
{
    [MenuItem("LTA/ClearDataCache")]
    private static void ClearDataCache()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    [MenuItem("LTA/RefreshJson")]
    private static void DoRefreshJson()
    {
        string combinePath = Path.Combine(Application.dataPath + "/Resources/Data/");
        string[] allFiles = Directory.GetFiles(combinePath, "*.*", SearchOption.AllDirectories);
        if (allFiles == null || allFiles.Length == 0) return;
        int length = allFiles.Length;
        for (int i = 0; i < length; i++)
        {
            string filePath = allFiles[i];
            filePath = filePath.Replace("\\", "/");
            if (!filePath.EndsWith(".json")) continue;
            string fileName = filePath.Substring(filePath.IndexOf("Data"));
            fileName = fileName.Substring(0, fileName.Length - 5);
            TextAsset text = Resources.Load<TextAsset>(fileName);
            JSONNode json = JSON.Parse(text.text)["data"];
            if (json != null && fileName == "Data/GameConfig")
            {                
                if (FormatJsonGameConfig(json))
                {
                    JSONObject jOb = new JSONObject();
                    jOb.Add("data", json.AsArray);
                    File.WriteAllText(allFiles[i], jOb.ToString());
                }
            }


            if (json == null)
            {
                JSONNode checkJson = JSON.Parse(text.text);
                if (fileName == "Data/GameConfig")
                {
                    FormatJsonGameConfig(checkJson);
                }
                JSONObject jOb = new JSONObject();
                if (checkJson.IsArray)
                {
                    jOb.Add("data", checkJson.AsArray);
                }
                else
                {
                    jOb.Add("data", checkJson.AsObject);
                }
                File.WriteAllText(allFiles[i], jOb.ToString());
            }
            
        }

    }
    private static bool FormatJsonGameConfig(JSONNode data)
    {
        JSONArray array;
        if (data["data"] != null)
            array = data["data"].AsArray;
        else
            array = data.AsArray;
        bool result = false;
        for (int i = 0; i < array.Count; i++)
        {
            var item = array[i];
            var leveldatas = item["levelDatas"].AsArray;
            for(int j = leveldatas.Count - 1; j >= 0; j--)
            {
                var level = leveldatas[j];
                if (level["level"].Tag == JSONNodeType.NullValue || level["rate"].Tag == JSONNodeType.NullValue)
                {
                    Debug.Log("remove null");
                    leveldatas.Remove(level);
                    result = true;
                }
            }
        }
        return result;
    }

    [MenuItem("LTA/DeleteUserData")]
    private static void DeleteUserData()
    {
        //SaveDataUtil.DeleteData("UserData");
        //SaveDataUtil.DeleteData("Abilities");
        PlayerPrefs.DeleteKey("IsFinishTutorial");
    }
    [MenuItem("LTA/RefreshNewJson")]
    private static void DoRefreshNewJson()
    {
        string combinePath = Path.Combine(Application.dataPath + "/Resources/Data/");
        string[] allFiles = Directory.GetFiles(combinePath, "*.*", SearchOption.AllDirectories);
        if (allFiles == null || allFiles.Length == 0) return;
        int length = allFiles.Length;
        for (int i = 0; i < length; i++)
        {
            string filePath = allFiles[i];
            filePath = filePath.Replace("\\", "/");
            if (!filePath.EndsWith(".json")) continue;
            string fileName = filePath.Substring(filePath.IndexOf("Data"));
            fileName = fileName.Substring(0, fileName.Length - 5);
            TextAsset text = Resources.Load<TextAsset>(fileName);
            JSONNode json = JSON.Parse(text.text)["data"];
            if (json != null && fileName == "Data/GameConfig")
            {                
                if (FormatJsonGameConfig(json))
                {
                    JSONObject jOb = new JSONObject();
                    jOb.Add("data", json.AsArray);
                    File.WriteAllText(allFiles[i], jOb.ToString());
                }
            }


            if (json == null)
            {
                JSONNode checkJson = JSON.Parse(text.text);
                string content = text.text.Trim();
                if (fileName == "Data/GameConfig")
                {
                    FormatJsonGameConfig(checkJson);
                }
                JSONObject jOb = new JSONObject();
                if (checkJson.IsArray)
                {
                    content = content.Substring(1, content.Length - 2);
                    Debug.Log(content);
                }
                else
                {
                    jOb.Add("data", checkJson.AsObject);
                }
                File.WriteAllText(allFiles[i], content);
            }
        
        }
    }
}


#endif
