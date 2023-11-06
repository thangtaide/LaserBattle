using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveInfo
{
    public float speed;
}


public class MoveController : MonoBehaviour,IOnUpLevel
{
    public float speed;

    protected bool isStop = false;

    public float Speed
    {
        get
        {
            return speed;
        }
    }
    public bool Stop
    {
        set
        {
            isStop = value;
        }
    }

    protected Vector3 direction;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }
    public virtual void Move(Vector3 direction)
    {
        IOnMove[] onMoves = GetComponentsInChildren<IOnMove>();
        foreach(IOnMove onMove in onMoves)
        {
            onMove.OnMove(direction);
        }
    }

    public void OnUpLevel(int level)
    {
        try
        {
            MoveInfo moveInfo = MoveDataController.Instance.moveVO.GetData<MoveInfo>(name, level);
            if (moveInfo == null) return;
            speed = moveInfo.speed;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    public virtual void Stand()
    {
        this.direction = Vector3.zero;
    }
}
