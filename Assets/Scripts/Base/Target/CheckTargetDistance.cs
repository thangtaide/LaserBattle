using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetDistance : MonoBehaviour, ICheckTarget
{
    [SerializeField] float maxDistance;
    public bool CheckTarget(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= maxDistance) return true;
        return false;
    }

}
