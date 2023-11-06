using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ElementsInfo
{
    public string[] elements;
}
public class ElementsController : MonoBehaviour,IOnUpLevel
{
    public string[] elements;
    public void OnUpLevel(int level)
    {
        if (!ElementsDataController.Instance.elementsVO.CheckKey(name)) return;
        ElementsInfo elemetsInfo = ElementsDataController.Instance.elementsVO.GetData<ElementsInfo>(name, level);
        if (elemetsInfo == null) return;
        elements = elemetsInfo.elements;
    }
}
