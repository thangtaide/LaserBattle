using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Diagnostics;

public interface ICondition
{
    public object Info { set; }
    public bool IsSuitable { get; }
    public Action<ICondition> OnSuitable { set; }
    public void ResetCondition();
}

public interface IHandle
{
    public object Info { set; }
    public void Handle();
}

public class SkillController : MonoBehaviour
{
    public string skillName;

    List<ICondition> conditions = new List<ICondition>();

    List<IHandle> handles = new List<IHandle>();

    void Start()
    {
        JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/Skills/" + skillName).text);
        JSONArray jsonConditions = json["data"]["conditions"].AsArray;
        JSONArray jsonHandles = json["data"]["handles"].AsArray;
        SubEffectInfo[] dataConditions = Utils.GetSubEffectInfos(jsonConditions);
        SubEffectInfo[] dataHandles = Utils.GetSubEffectInfos(jsonHandles);
        foreach (SubEffectInfo conditonInfo in dataConditions)
        {
            ICondition condition = (ICondition)gameObject.AddComponent(conditonInfo.type);
            condition.Info = conditonInfo.data;
            condition.OnSuitable = OnSuitable;
            conditions.Add(condition);
        }
        foreach (SubEffectInfo handleInfo in dataHandles)
        {
            IHandle handle = (IHandle)gameObject.AddComponent(handleInfo.type);
            handle.Info = handleInfo.data;
            handles.Add(handle);
        }

    }

    void OnSuitable(ICondition condition)
    {
        if (!CheckConditions()) return;
        foreach (ICondition condition1 in conditions)
        {
            condition1.ResetCondition();
        }
        foreach (IHandle handle in handles)
        {
            handle.Handle();
        }
    }

    bool CheckConditions()
    {
        foreach (ICondition condition in conditions)
        {
            if (!condition.IsSuitable) return false;
        }
        return true;
    }
}
