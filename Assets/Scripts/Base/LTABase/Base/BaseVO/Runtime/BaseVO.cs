using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using System;
namespace LTA.VO
{
    public class BaseVO
    {
        public JSONNode data;

        public virtual void LoadDataByName(string dataName)
        {
            string text = GlobalVO.GetDataText.GetDataText(dataName);
            LoadData(text);
        }

        public virtual void LoadData(string text)
        {
            data = JSON.Parse(text)["data"];
        }

        public bool LoadData(string text, string key)
        {
            JSONNode currentData = JSON.Parse(text)["data"];
            if (currentData.IsArray)
                return LoadData(currentData.AsArray, key);
            else
                data = currentData[key];
                return true;
        }

        bool LoadData(JSONArray array, string key)
        {
            JSONArray newData = new JSONArray();
            int countNull = 0;
            for (int i = 0; i < array.Count; i++)
            {
                JSONObject json = array[i].AsObject;
                if (json[key] == null) countNull++;
                newData.Add(array[i].AsObject[key]);
            }
            if (countNull == newData.Count) return false;
            data = newData;
            return true;
        }

        public virtual T GetData<T>(int level)
        {
            JSONArray array = data.AsArray;
            if (level > array.Count) return JsonUtility.FromJson<T>(array[array.Count - 1].ToString());
            return JsonUtility.FromJson<T>(array[level - 1].ToString());
        }
        public virtual T GetData<T>()
        {
            return JsonUtility.FromJson<T>(data.ToString());
        }
        public List<T> GetDatas<T>()
        {
            List<T> results = new List<T>();
            JSONArray array = data.AsArray;
            for (int i = 0; i < array.Count; i++)
            {
                T data = JsonUtility.FromJson<T>(array[i].ToString());
                results.Add(data);
            }
            return results;
        }
        
        public int Count
        {
            get
            {
                return data.AsArray.Count;
            }
            
        }
        
        public List<T> GetDatas<T>(int level)
        {
            JSONArray arrayAll = data.AsArray;
            JSONArray array;
            if (level > arrayAll.Count)
            {
                array = arrayAll[arrayAll.Count - 1].AsArray;
            }
            else
            {
                array = arrayAll[level - 1].AsArray;
            }
            List<T> results = new List<T>();

            for (int i = 0; i < array.Count; i++)
            {
                T waveInfo = JsonUtility.FromJson<T>(array[i].ToString());
                results.Add(waveInfo);
            }
            return results;
        }

        public object GetData(string key, Type type)
        {
            JSONObject json = data.AsObject;
            if (json[key].IsNull) return null;
            return JsonUtility.FromJson(json[key].ToString(), type);
        }

        public JSONObject GetData(int level)
        {
            JSONArray array = data.AsArray;
            if (level > array.Count) return array[array.Count - 1].AsObject;
            return array[level - 1].AsObject;
        }
    }
}
