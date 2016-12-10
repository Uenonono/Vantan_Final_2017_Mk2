using UnityEngine;
using System.Collections;

//マ○オ 敵
public class EnemyAF : MonoBehaviour
{
    public int hp;  //HP
    public float speed;     //スピード
    public float activeTime;    //青くなるまでの時間
    bool isDown;    //状態
    bool isActive;  //青状態


    void Start()
    {

    }


    void Update()
    {
        if (!isDown && isActive)
        {
            speed = 0.15f;  //とりあえず
            //移動
            transform.Translate(speed, 0, 0);
            Debug.Log("発狂状態");
        }
        else
        {
            //移動
            transform.Translate(speed, 0, 0);
        }


        if (isDown)
        {
            speed = 0;
            activeTime -= Time.deltaTime;

            if (activeTime <= 0)
            {
                isDown = false;
                isActive = true;
                Debug.Log("状態チェンジ");
            }
        }
    }


    //当たり判定
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Attack")
        {
            Debug.Log("当たった");
            isDown = true;
        }
    }
}
