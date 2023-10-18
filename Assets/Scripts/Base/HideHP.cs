using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHP : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Color originColor;
    float timeOnHit;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;

        spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0);
    }
    public void ShowAlpha()
    {
        timeOnHit = Time.time;
        spriteRenderer.color = originColor;
    }
    private void Update()
    {
        if (Time.time - timeOnHit >= 3)
        {
            spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0);
        }
    }
}
