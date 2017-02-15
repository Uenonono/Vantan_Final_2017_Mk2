using UnityEngine;
using System.Collections;

//シューティング エネミー
public class STGEnemy : MonoBehaviour
{
    public bool Normal;   //通常エネミー
    public bool Attacker;   //攻撃エネミー
    public bool Shielder;   //盾エネミー

    public float speed; //移動速度
    public int getScore;    //スコア
    public GameObject Piece;    //死亡エフェクト
    public GameObject Effect;    //死亡エフェクト

    public GameObject Shot; //弾
    public float intervalTime;  //発射間隔
    float interval;

    public static int setScore;

    //敵の状態
    enum EnemyState
    {
        Summon, //召喚
        Normal,  //通常
        Active,   //異常
    }
    EnemyState enemyState = EnemyState.Summon;


    //状態チェンジまでの時間
    public float changeTime;
    float change;
    public float activeTime;
    float active;

    public bool isTitle;

    float moveX = 0.05f;
    float moveTime;
    float move;

    public GameObject EffectS;    //登場エフェクト


    void Start()
    {
        interval = 0;
        change = 0;
        active = 0;

        setScore = getScore;

        //サウンドロードSpawn
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");


        //エフェクト
        Instantiate(EffectS, new Vector3(transform.position.x,
                                       transform.position.y,
                                       transform.position.z),
                                       Quaternion.identity);
    }


    void Update()
    {
        if (Normal) { NormalMove(); }
        if (Attacker) { AttackMove(); }
        if (Shielder) { ShieldMove(); }

        if (STGBoss.isDead)
        {
            //エフェクトピース
            Instantiate(Piece, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //エフェクト
            Instantiate(Effect, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            SoundMgr.PlaySe("Death", 2);

            Destroy(this.gameObject);
        }
    }


    //行動
    public void NormalMove()
    {
        switch (enemyState)
        {
            //召喚
            case EnemyState.Summon:
                change += Time.deltaTime;
                if (change >= changeTime)
                {
                    enemyState = EnemyState.Normal;
                }
                break;
            //通常
            case EnemyState.Normal:
                //ステータス変更
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


    public void AttackMove()
    {
        switch (enemyState)
        {
            //召喚
            case EnemyState.Summon:
                change += Time.deltaTime;
                if (change >= changeTime)
                {
                    enemyState = EnemyState.Normal;
                }
                break;
            //通常
            case EnemyState.Normal:
                //ステータス変更
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
                //移動
                move += Time.deltaTime;
                if (move >= 0)
                {
                    transform.Translate(moveX, 0, speed * 3.0f);
                }
                if (move >= 2)
                {
                    transform.Translate(-moveX * 2, 0, speed * 3.0f);
                }
                if (move >= 4)
                {
                    move = 0.0f;
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


    public void ShieldMove()
    {
        switch (enemyState)
        {
            //召喚
            case EnemyState.Summon:
                change += Time.deltaTime;
                if (change >= changeTime)
                {
                    enemyState = EnemyState.Normal;
                }
                break;
            //通常
            case EnemyState.Normal:
                //ステータス変更
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
                move += Time.deltaTime;
                if (move >= 0)
                {
                    transform.Translate(moveX, 0, 0);
                }
                if (move >= 2)
                {
                    transform.Translate(-moveX * 2, 0, 0);
                }
                if (move >= 4)
                {
                    move = 0.0f;
                }
                break;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            if (!STGPlayer.isDead && !STGBoss.isDead)
            {
                //スコア
                Score.score += getScore;
            }

            //エフェクトピース
            Instantiate(Piece, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //エフェクト
            Instantiate(Effect, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            SoundMgr.PlaySe("Death", 2);

            Destroy(this.gameObject);
        }


        //壁
        if (collision.gameObject.tag == "Dead")
        {
            if (!STGPlayer.isDead && Score.score > 0)
            {
                //スコア
                Score.score -= getScore;
            }

            Destroy(this.gameObject);
        }
    }
}
