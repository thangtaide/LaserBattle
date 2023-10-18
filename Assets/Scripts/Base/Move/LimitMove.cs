using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMove : MonoBehaviour
{
    public Vector2 minLimit;
    public Vector2 maxLimit;
    void Update()
    {
        float newPositionX = Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x);
        float newPositionY = Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y);
        transform.position = new Vector3(newPositionX, newPositionY);
    }
}
