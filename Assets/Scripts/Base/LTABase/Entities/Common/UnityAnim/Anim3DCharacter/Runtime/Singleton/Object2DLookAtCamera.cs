using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object2DLookAtCamera : MonoBehaviour
{
    Transform camera;
    // Start is called before the first frame update
    private void Awake()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + camera.rotation * Vector3.back, camera.rotation * Vector3.up);
    }
}
