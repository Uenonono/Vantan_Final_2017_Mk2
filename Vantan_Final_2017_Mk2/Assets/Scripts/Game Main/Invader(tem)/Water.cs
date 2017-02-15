using UnityEngine;
using System.Collections;

//テクスチャを動かす
public class Water : MonoBehaviour
{
    Renderer rend;

    float scrollSpeed = 0.25F;      //速度


    void Start()
    {
        rend = GetComponent<Renderer>();
    }


    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
