using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
namespace LTA.VO
{
    public class BaseMutilVO
    {
        public Dictionary<string, BaseVO> dic_Data = new Dictionary<string, BaseVO>();

        public bool CheckKey(string key)
        {
            return dic_Data.ContainsKey(key);
        }

        protected virtual void LoadData<T>(string dataName) where T : BaseVO, new()
        {
            DataTextInfo[] dataTexts = GlobalVO.GetDataTexts.GetDataTexts(dataName);
            foreach (DataTextInfo text in dataTexts)
            {
                T data = new T();
                data.LoadData(text.data);
                dic_Data.Add(text.name, data);
            }
        }

        protected virtual void LoadData<T>(string dataName, string key) where T : BaseVO, new()
        {
            DataTextInfo[] dataTexts = GlobalVO.GetDataTexts.GetDataTexts(dataName);
            foreach (DataTextInfo text in dataTexts)
            {
                T data = new T();
                if (data.LoadData(text.data, key))
                {
                    dic_Data.Add(text.name, data);
                }
            }
        }

        public virtual T GetData<T>(string type, int level)
        {
            if (!dic_Data.ContainsKey(type)) throw new System.Exception("data "+ type + " is null");
            return dic_Data[type].GetData<T>(level);
        }

        public virtual List<T> GetDatas<T>(string type)
        {
            if (!dic_Data.ContainsKey(type)) throw new System.Exception("data " + type + " is null");
            return dic_Data[type].GetDatas<T>();
        }
        public virtual int GetCount(string type)
        {
            if (!dic_Data.ContainsKey(type)) throw new System.Exception("data " + type + " is null");
            return dic_Data[type].Count;
        }
        
        public virtual T GetData<T>(string type)
        {
            if (!dic_Data.ContainsKey(type)) throw new System.Exception("data " + type + " is null");
            return dic_Data[type].GetData<T>();
        }

        public virtual List<T> GetDatas<T>(string type, int level)
        {
            if (!dic_Data.ContainsKey(type)) throw new System.Exception("data " + type + " is null");
            return dic_Data[type].GetDatas<T>(level);
        }

        public JSONObject GetData(string type, int level)
        {
            if (!dic_Data.ContainsKey(type)) throw new System.Exception("data " + type + " is null");
            return dic_Data[type].GetData(level);
        }
    }
}
