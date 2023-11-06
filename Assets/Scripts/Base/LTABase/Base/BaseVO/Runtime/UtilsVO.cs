using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LTA.Base;
using LTA.VO;
using SimpleJSON;
using UnityEngine;
[System.Serializable]
public class SubInfo
{
    public Assembly assembly;
    public string type;
    public object data;
}

public class UtilsVO
{
    public static SubInfo[] GetSubInfos(JSONArray infos)
    {
        SubInfo[] battleEffects = new SubInfo[infos.Count];
        for (int i = 0; i < infos.Count; i++)
        {
            battleEffects[i] = new SubInfo();
            string assemblyName = infos[i]["assemblyName"];
            Assembly assembly = Assembly.Load(assemblyName);
            battleEffects[i].assembly = assembly;
            battleEffects[i].type = infos[i]["type"];
            if (infos[i]["typeInfo"] == null) continue;
            string typeInfo = infos[i]["typeInfo"];
            battleEffects[i].data = JsonUtility.FromJson(infos[i]["data"].ToString(), assembly.GetType(typeInfo));
        }
        return battleEffects;
    }
    public static void AddSubInfos(GameObject gameObject,SubInfo[] subInfos, Action<Component> editEffect = null)
    {
        for (int i = 0; i < subInfos.Length; i++)
        {
            SubInfo subInfo = subInfos[i];
            Assembly assembly = subInfo.assembly;
            Component component = gameObject.AddComponent(assembly.GetType(subInfo.type));
            if (subInfo.data == null)
            {
                if (editEffect != null)
                    editEffect(component);
                continue;
            }
            ISetInfo setInfo = (ISetInfo)component;
            setInfo.Info = subInfo.data;
            if (editEffect != null)
                editEffect(component);
        }
    }
}
