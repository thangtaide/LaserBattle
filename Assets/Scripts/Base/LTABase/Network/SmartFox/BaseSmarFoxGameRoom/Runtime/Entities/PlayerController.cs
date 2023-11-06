using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public bool isMine;

    bool isProcessing = false;

    public Queue<ISFSObject> queueAction = new Queue<ISFSObject>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isProcessing) return;
        isProcessing = true;

        if (queueAction.Count == 0)
        {
            isProcessing = false;
            return;
        }

        ISFSObject sfsObject = queueAction.Dequeue();
        OnAction(sfsObject);
        isProcessing = false;
    }

    protected abstract void OnAction(ISFSObject sfsObject);
}
