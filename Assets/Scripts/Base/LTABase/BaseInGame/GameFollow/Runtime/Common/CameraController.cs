using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Vector2 size = new Vector2(
        ((float)Screen.height / Screen.width) / ((float)9 / 16),
        1f
        );
        Vector2 position = new Vector2(
            (1 - size.x) / 2,
            0f
            );
        //transform.position += Vector3.left *(1 - size.x);
        GetComponent<Camera>().rect = new Rect(position, size);
    }

}
