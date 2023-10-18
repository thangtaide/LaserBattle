using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClearPath : MonoBehaviour, ICheckTarget
{
    public bool CheckTarget(Transform target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position- transform.up*1.5f, target.position - transform.position, 50f);
        if(hit.transform != target)
        {
            return false;
        }
        return true;
    }

}
