using UnityEngine;
using System.Collections;

//シューティング プレイヤー
public class STGPlayer : MonoBehaviour
{
    public float speed; //移動速度
    float interval;
    public float intervalTime;  //発射間隔
    public GameObject Shot; //弾

    public static bool isDead;  //生死フラグ


    void Start()
    {
        interval = 0;
        isDead = false;

        //サウンドロード
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
    }


    void Update()
    {
        Move();
    }


    //プレイヤーの行動
    void Move()
    {
        //左右移動
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // 移動する向きを求める
        Vector3 direction = new Vector3(x, 0, z).normalized;
        // 移動する向きとスピードを代入する
        GetComponent<Rigidbody>().velocity = direction * speed;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -20, 20);
        pos.z = Mathf.Clamp(pos.z, -20, -10);
        //制限をかけた値をプレイヤーの位置とする
        transform.position = pos;

        //ショット
        interval += Time.deltaTime;
        if (Input.GetKey("space"))
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

            //音
            SoundMgr.PlaySe("Death", 4);

            //当たったら消える
            Destroy(this.gameObject);
        }
    }
}
