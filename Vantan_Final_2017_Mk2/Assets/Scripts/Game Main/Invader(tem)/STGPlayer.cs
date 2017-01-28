using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//シューティング プレイヤー
public class STGPlayer : MonoBehaviour
{
    Rigidbody _rigidbody;
    public float speed; //移動速度
    float interval;
    public float intervalTime;  //発射間隔
    public GameObject Shot; //弾

    public GameObject Pice; //エフェクト
    public GameObject Effect;    //死亡エフェクト

    public static bool isDead;  //生死フラグ


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        interval = 0;

        isDead = false;

        //サウンドロード
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
    }


    void Update()
    {
        if(GameTime.isCount)
        {
            Move();
        }
    }


    //プレイヤーの行動
    void Move()
    {
        //左右移動
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // 移動する向きを求める
        Vector3 direction = new Vector3(x, 0, z);
        // 移動する向きとスピードを代入する
        _rigidbody.velocity = direction * speed;

        //制限をかけた値をプレイヤーの位置とする
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -20, 20);
        pos.z = Mathf.Clamp(pos.z, -20, -10);
        transform.position = pos;

        //ショット
        interval += Time.deltaTime;
        if (Input.GetAxis("BottomRed") == 1)
        {
            if (interval >= intervalTime)
            {
                interval = 0.0f;

                Instantiate(Shot, new Vector3(transform.position.x,
                                              transform.position.y,
                                              transform.position.z + 1.5f),
                                              Quaternion.identity);

                //音
                SoundMgr.PlaySe("Shot", 3);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        //敵、敵の弾、弾に当たったらゲームオーバー
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shot" || collision.gameObject.tag == "EnemyShot")
        {
            isDead = true;

            //Instantiate(Pice, new Vector3(transform.position.x,
            //                  transform.position.y,
            //                  transform.position.z),
            //                  Quaternion.identity);

            //エフェクト
            Instantiate(Effect, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            SoundMgr.PlaySe("Death", 4);

            //当たったら消える
            Destroy(this.gameObject);
        }
    }
}
