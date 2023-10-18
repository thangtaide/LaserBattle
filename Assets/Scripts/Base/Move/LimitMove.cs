using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMove : MonoBehaviour
{
    public float scaleLimitleft;
    public float scaleLimitRight;
    public float scaleLimitTop;
    public float scaleLimitBottom;
    public Vector2 minLimit;
    public Vector2 maxLimit;
    float cameraHalfHeigh;
    float cameraHalfWidth;
    private void Start()
    {
        cameraHalfHeigh = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeigh * Camera.main.aspect;

        minLimit = new Vector2(-cameraHalfWidth * scaleLimitleft, -cameraHalfHeigh * scaleLimitBottom);
        maxLimit = new Vector2(cameraHalfWidth * scaleLimitRight, cameraHalfHeigh * scaleLimitTop);
    }
    void Update()
    {
        float newPositionX = Mathf.Clamp(transform.position.x, minLimit.x,maxLimit.x );
        float newPositionY = Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y);
        transform.position = new Vector3(newPositionX, newPositionY);
    }
}
