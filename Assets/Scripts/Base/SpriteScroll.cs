using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScroll : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] bool isImage = false;
    Vector2 offset;
    Material material;
    void Start()
    {
        if (isImage)
        {
            material = GetComponent<Image>().material;
        }
        else
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
