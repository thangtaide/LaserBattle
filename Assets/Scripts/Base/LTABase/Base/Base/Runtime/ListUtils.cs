using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using UnityEngine;

using System;

public class ListUtils<T> where T : class
{
    public static Dictionary<string, List<T>> dic_Name_ListObj = new Dictionary<string, List<T>>();

    public static List<T> GetListObj(string listName)
    {
        if (dic_Name_ListObj.ContainsKey(listName))
        {
            return dic_Name_ListObj[listName];
        }
        return null;
    }

    public static void AddObjRespond(string listName, T obj)
    {
        List<T> listObj = GetListObj(listName);
        if (listObj == null)
        {
            listObj = new List<T>();
            dic_Name_ListObj.Add(listName, listObj);
        }
        listObj = dic_Name_ListObj[listName];
        if (!listObj.Contains(obj))
        {
            listObj.Add(obj);
        }
        Observer.Instance.Notify(listName,listObj);
    }
    public static void RemoveObject(string listName, T obj)
    {
        List<T> listObj = GetListObj(listName);
        if (listObj == null) return;
        if (listObj.Contains(obj))
        {
            listObj.Remove(obj);
            Observer.Instance.Notify(listName,listObj);
        }
    }

    public static T GetObj(string listName, string itemName,Func<T,bool> checkObj)
    {
        List<T> listObj = GetListObj(listName);
        if (listObj == null) return null;
        foreach (T obj in listObj)
        {
            if (checkObj(obj))
            {
                return obj;
            }
        }
        return null;
    }

}

