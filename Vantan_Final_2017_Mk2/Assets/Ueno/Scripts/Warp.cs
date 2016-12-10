using UnityEngine;
using System.Collections;

//ワープ
public class Warp : MonoBehaviour
{
    //移動先チャンネル
    public float warpX;
    public float warpY;


    //当たり判定
    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.transform.position = new Vector2(warpX, warpY);
    }
}
