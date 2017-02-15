using UnityEngine;
using System.Collections;

//シューティング エネミー
public class STGEnemy : MonoBehaviour
{
    //チェックを入れるとその敵の行動をする
    public bool Normal;   //通常エネミー
    public bool Attacker;   //攻撃エネミー
    public bool Shielder;   //盾エネミー

    public float speed; //移動速度

    public int getScore;    //スコア

    //ショット
    public GameObject Shot; //弾
    public float intervalTime;  //発射間隔
    float interval;

    public GameObject EffectSummon;    //登場エフェクト
    public GameObject EffectDead;    //死亡エフェクト


    //敵の状態
    enum EnemyState
    {
        Normal,  //通常
        Active,   //異常
    }
    EnemyState enemyState = EnemyState.Normal;


    //アクティブまでの時間
    public float activeTime;
    float active;

    public bool isTitle;

    //ジグザグ移動とカニ移動
    float moveX = 0.05f;    //X値
    float moveTime; //移動


    void Start()
    {
        interval = 0;
        active = 0;

        //登場エフェクト
        Instantiate(EffectSummon, new Vector3(transform.position.x,
                                       transform.position.y,
                                       transform.position.z),
                                       Quaternion.identity);

        //サウンドロード
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
    }


    void Update()
    {
        //チェックしたの敵の行動をする
        if (Normal) { NormalMove(); }
        if (Attacker) { AttackMove(); }
        if (Shielder) { ShieldMove(); }


        //ボスが死んでたら死ぬ
        if (STGBoss.isDead)
        {
            //死亡エフェクト
            Instantiate(EffectDead, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            SoundMgr.PlaySe("Death", 2);

            Destroy(this.gameObject);
        }
    }


    //緑行動
    public void NormalMove()
    {
        switch (enemyState)
        {
            //通常
            case EnemyState.Normal:
                active += Time.deltaTime;
                if (active >= activeTime)
                {
                    enemyState = EnemyState.Active;
                }

                //移動
                transform.Translate(0, 0, speed);
                break;


            //時間経過
            case EnemyState.Active:
                //移動
                transform.Translate(0, 0, speed * 3.0f);
                break;
        }
    }


    //赤行動
    public void AttackMove()
    {
        switch (enemyState)
        {
            //通常
            case EnemyState.Normal:
                active += Time.deltaTime;
                if (active >= activeTime)
                {
                    enemyState = EnemyState.Active;
                }

                //移動
                transform.Translate(0, 0, speed);

                //攻撃
                interval += Time.deltaTime;
                if (interval >= intervalTime)
                {
                    interval = 0.0f;

                    //弾
                    Instantiate(Shot, new Vector3(transform.position.x,
                                              transform.position.y,
                                              transform.position.z - 1),
                                              Quaternion.identity);

                    //音
                    SoundMgr.PlaySe("Shot", 1);
                }
                break;


            //時間経過
            case EnemyState.Active:
                //ジグザグ移動
                moveTime += Time.deltaTime;
                if (moveTime >= 0)
                {
                    transform.Translate(moveX, 0, speed * 3.0f);
                }
                if (moveTime >= 2)
                {
                    transform.Translate(-moveX * 2, 0, speed * 3.0f);
                }
                if (moveTime >= 4)
                {
                    moveTime = 0.0f;
                }

                //攻撃
                interval += Time.deltaTime;
                if (interval >= 4)
                {
                    interval = 0.0f;

                    //弾
                    Instantiate(Shot, new Vector3(transform.position.x,
                                                  transform.position.y,
                                                  transform.position.z - 1),
                                                  Quaternion.identity);

                    //音
                    SoundMgr.PlaySe("Shot", 1);
                }
                break;
        }
    }


    //青行動
    public void ShieldMove()
    {
        switch (enemyState)
        {
            //通常
            case EnemyState.Normal:
                active += Time.deltaTime;
                if (active >= activeTime)
                {
                    enemyState = EnemyState.Active;
                }

                //移動
                transform.Translate(0, 0, speed);
                break;


            //時間経過
            case EnemyState.Active:
                //カニ移動
                moveTime += Time.deltaTime;
                if (moveTime >= 0)
                {
                    transform.Translate(moveX, 0, 0);
                }
                if (moveTime >= 2)
                {
                    transform.Translate(-moveX * 2, 0, 0);
                }
                if (moveTime >= 4)
                {
                    moveTime = 0.0f;
                }
                break;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの弾が当たったら
        if (collision.gameObject.tag == "Shot")
        {
            if (!STGPlayer.isDead && !STGBoss.isDead)
            {
                //スコア
                Score.score += getScore;
            }

            //死亡エフェクト
            Instantiate(EffectDead, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            SoundMgr.PlaySe("Death", 2);

            Destroy(this.gameObject);
        }


        //壁にぶつかるとスコア減る
        if (collision.gameObject.tag == "Dead")
        {
            if (!STGPlayer.isDead && Score.score >= 0)
            {
                Score.score -= getScore;
            }
            Destroy(this.gameObject);
        }
    }
}
