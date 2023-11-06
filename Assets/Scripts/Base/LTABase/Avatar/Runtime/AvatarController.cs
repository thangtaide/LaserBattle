using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class AvatarInfo
{
    public string path;
}
[RequireComponent(typeof(Image))]
public class AvatarController : MonoBehaviour,IOnUpLevel
{
    [SerializeField]
    string typeAvatar;
    [SerializeField]
    Sprite defautAvatar;
    [SerializeField]
    Image image;
    public void OnUpLevel(int level)
    {
        if (image == null) return;
        try
        {
            AvatarInfo avatarInfo = AvatarDataController.Instance.GetAvatarVO(typeAvatar).GetData<AvatarInfo>(name,level);
            Sprite sprite = GlobalVO.GetAsset.GetAsset<Sprite>(avatarInfo.path);
            image.sprite = sprite;
        }
        catch (Exception e)
        {
            Debug.LogError("Error Avatar " + e.Message);
            image.sprite = defautAvatar;
        }
    }
}
