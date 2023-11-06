using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Base.Character.UnityAnim;
[System.Serializable]
public class FrameByFrameInfo
{
    public string path;
    public int layer = -1;
}
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class FrameByFrameCharacter : UnityAnimCharacter
{
    SpriteRenderer spriteRenderer;

    public override float Alpha { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
    }

    public override void OnUpLevel(int level)
    {
        FrameByFrameInfo frameByFrameInfo = FrameByFrameDataController.Instance.frameByFrameVO.GetData<FrameByFrameInfo>(OwnName,level);
        if (frameByFrameInfo == null) return;
        if (frameByFrameInfo.layer != -1)
            SpriteRenderer.sortingLayerID = frameByFrameInfo.layer;
        RuntimeAnimatorController controller = GlobalVO.GetAsset.GetAsset<RuntimeAnimatorController>(frameByFrameInfo.path);
        Animator.runtimeAnimatorController = controller;
        base.OnUpLevel(level);

    }
}
