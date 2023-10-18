using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectInfo
{
    public string path;
}

public class AddObjectHandle : MonoBehaviour, IHandle
{
    AddObjectInfo info;

    public object Info { set => info = (AddObjectInfo)value; }

    public void Handle()
    {
        GameObject prefab = Resources.Load<GameObject>(info.path);
        if (prefab != null)
            Instantiate(prefab, transform.position, transform.rotation);
    }
}
