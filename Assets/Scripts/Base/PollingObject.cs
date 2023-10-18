using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.DesignPattern
{
    public class PollingObject : Singleton<PollingObject>
    {
        public static Dictionary<string, List<MonoBehaviour>> dic_PollingObject = new Dictionary<string, List<MonoBehaviour>>();

        public static void DestroyPolling<Obj>(Obj _obj)where Obj : MonoBehaviour
        {
            string name = _obj.gameObject.name;
            if (dic_PollingObject.ContainsKey(name))
            {
                _obj.gameObject.SetActive(false);
                dic_PollingObject[name].Add(_obj);
                return;
            }
            GameObject.Destroy(_obj.gameObject);
        }

        public static Obj CreatePolling<Obj>(Obj obj) where Obj : MonoBehaviour
        {
            string Type = obj.gameObject.name+"(Clone)";
            List<MonoBehaviour> list;
            if (!dic_PollingObject.ContainsKey(Type))
            {
                list = new List<MonoBehaviour>();
                dic_PollingObject[Type] = list;
                return null;
            }
            else
            {
                list = dic_PollingObject[Type];
                if(list.Count > 0)
                {
                    Obj _obj = (Obj)list[0];
                    list.RemoveAt(0);
                    _obj.gameObject.SetActive(true);
                    return _obj;
                }
                return null;
            }
        }
    }
}