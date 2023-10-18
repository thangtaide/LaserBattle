using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector2 minLimit;
    public Vector2 maxLimit;
    private Camera cameraMain;
    private float cameraHalfHeigh;
    private float cameraHalfWidth;
    private void Start()
    {
        cameraMain = Camera.main;
        cameraHalfHeigh = cameraMain.orthographicSize;
        cameraHalfWidth = cameraHalfHeigh * cameraMain.aspect;
    }
    
    private void LateUpdate()
    {
        if (Player.Instance != null)
        {
            float targerX = Player.Instance.transform.position.x;
            float targerY = Player.Instance.transform.position.y;
            float newCameraPositionX = Mathf.Clamp(targerX, minLimit.x + cameraHalfWidth, maxLimit.x - cameraHalfWidth);
            float newCameraPositionY = Mathf.Clamp(targerY, minLimit.y + cameraHalfHeigh, maxLimit.y - cameraHalfHeigh);
            float newCameraPositionZ = cameraMain.transform.position.z;
            cameraMain.transform.position = new Vector3(newCameraPositionX, newCameraPositionY, newCameraPositionZ);
        }
    }
}
