using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//シューティング プレイヤー
public class STGPlayer : MonoBehaviour
{
    Rigidbody _rigidbody;
    public float speed; //移動速度

    //ショット
    public GameObject Shot; //弾
    public float intervalTime;  //発射間隔
    float interval;

    public GameObject EffectDead;       //死亡エフェクト

    public static bool isDead;  //生死フラグ
    public bool isRule; //ルール画面かどうか


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        interval = 0;

        isDead = false;

        //サウンドロード
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
        SoundMgr.SoundLoadSe("End", "Invader/End");
    }


    void Update()
    {
        if (GameTime.isCount || isRule)
        {
            Move();
        }
    }


    //プレイヤーの行動
    void Move()
    {
        if (isRule)
        {
            //もしルール画面なら
            float rx = Input.GetAxis("Horizontal");
            float ry = Input.GetAxis("Vertical");

            //移動する向きを求める
            Vector3 rdirection = new Vector3(rx, ry, 0);
            //移動する向きとスピードを代入する
            _rigidbody.velocity = rdirection * speed;

            //制限をかけた値をプレイヤーの位置とする
            Vector3 rpos = transform.position;
            rpos.x = Mathf.Clamp(rpos.x, 4, 8);
            rpos.y = Mathf.Clamp(rpos.y, 3, 5);
            transform.position = rpos;
        }
        else
        {
            //左右移動
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //移動する向きを求める
            Vector3 direction = new Vector3(x, 0, z);
            //移動する向きとスピードを代入する
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
                                                  transform.position.z + 2.5f),
                                                  Quaternion.identity);

                    //音
                    SoundMgr.PlaySe("Shot", 1);
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        //敵、敵の弾、弾に当たったらゲームオーバー
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shot" || collision.gameObject.tag == "EnemyShot")
        {
            isDead = true;

            //死亡エフェクト
            Instantiate(EffectDead, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            SoundMgr.PlaySe("Death", 2);
            SoundMgr.PlaySe("End", 3);

            //当たったら消える
            Destroy(this.gameObject);
        }
    }
}
