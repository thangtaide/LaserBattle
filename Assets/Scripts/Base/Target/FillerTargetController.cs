using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public interface ICheckTarget
{
    public bool CheckTarget(Transform target);
}
public class FillerTargetController : MonoBehaviour
{
    ICheckTarget[] checkTargets;
    void Start()
    {
        checkTargets = GetComponents<ICheckTarget>();
    }

    public bool CheckTarget(Transform target)
    {
        if (checkTargets == null || checkTargets.Length == 0) {
            return true; }
        foreach (ICheckTarget checkTarget in checkTargets) { 
            if(!checkTarget.CheckTarget(target)) {  
                return false; }
        }
        return true;
    }
}
