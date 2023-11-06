using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TypeInfo
{
    public string type;
}

public class LTAType : MonoBehaviour,IOnUpLevel
{
    public string type;

    public void OnUpLevel(int level)
    {
        if (!LTATypeDataController.Instance.typeVO.CheckKey(name)) return;
        TypeInfo typeInfo = LTATypeDataController.Instance.typeVO.GetData<TypeInfo>(name, level);
        if (typeInfo == null) return;
        type = typeInfo.type;
    }
}
