using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConeTarget : MonoBehaviour, ICheckTarget
{
    [SerializeField] float coneAngle;

    public bool CheckTarget(Transform target)
    {
        Vector3 directtionToTarget = target.position - transform.position;
        float angleToTarget = Vector3.Angle(directtionToTarget, -transform.up);//Sung nguoc huong
        if (angleToTarget > coneAngle / 2) return false;
        return true;
    }

}
