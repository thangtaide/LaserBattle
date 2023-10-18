using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZigZag : MonoBehaviour, ITypeMove
{
    Vector3 direction = Vector3.zero;
    float rightDirection = -10f;

    /*private void Start()
    {
        direction = transform.right.normalized * 7f;
    }*/
    public Vector3 Move()
    {
        if (direction.magnitude >= 5f)
        {
            rightDirection = -rightDirection;
        }
        direction += rightDirection * transform.right.normalized * Time.deltaTime;
        return direction;
    }
}
