using UnityEngine;
using System.Collections;

//アト プレイヤー
public class PlayerAF : MonoBehaviour
{
    public int hp;  //HP
    public GameObject attack;   //攻撃


    void Start()
    {

    }


    void Update()
    {

    }


    //エネミーとの当たり判定
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("死んだ!!");
            hp--;
        }
    }
}
