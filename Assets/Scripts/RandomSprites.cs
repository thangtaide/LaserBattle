using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprites : MonoBehaviour
{
    
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Texture2D[] textures = Resources.LoadAll<Texture2D>("Sprites/Enemies");
        int random = Random.Range(0, textures.Length);

        spriteRenderer.sprite = Sprite.Create(textures[random], new Rect(0, 0, textures[random].width, textures[random].height), Vector2.one * 0.5f);
    }
}
