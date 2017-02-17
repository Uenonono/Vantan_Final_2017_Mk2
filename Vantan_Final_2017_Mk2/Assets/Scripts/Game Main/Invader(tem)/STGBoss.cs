using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//ボス
public class STGBoss : MonoBehaviour
{
    Slider _Slider;
    public int hp;  //hp
    int HP;

    public int getScore;    //スコア

    //敵召喚
    public GameObject Enemy;    //敵
    public float spawnRange; //スポーン範囲

    //ショット
    public GameObject Shot; //弾
    public float intervalTime;  //発射間隔
    float interval;

    public GameObject EffectDead;   //死亡エフェクト

    public static bool isDead;  //生死フラグ


    //敵の状態
    enum EnemyState
    {
        Normal,  //通常
        Active,   //異常
    }
    EnemyState enemyState = EnemyState.Normal;


    //状態チェンジまでの時間
    public float activeTime;
    float active;

    //死亡までの時間
    float DeadTime = 2;
    float Dead;


    void Start()
    {
        _Slider = GameObject.Find("BossHPSlider").GetComponent<Slider>();
        HP = hp;

        //Sliderに値を設定
        _Slider.maxValue = HP;
        _Slider.value = HP;

        isDead = false;

        StartCoroutine(HPBer());

        //サウンドロード
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
        SoundMgr.SoundLoadSe("Spawn", "Invader/Spawn");
        SoundMgr.SoundLoadSe("ClearFanfare", "Invader/ClearFanfare");
    }


    void Update()
    {
        Move();

        //ボスをエフェクトの爆破と共に消す
        if (isDead)
        {
            Dead += Time.deltaTime;
        }
        if (Dead >= DeadTime)
        {
            Destroy(this.gameObject);
        }
    }


    public void Move()
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

                //敵スポーン3s
                interval += Time.deltaTime;
                if (interval >= intervalTime)
                {
                    interval = 0.0f;

                    Instantiate(Enemy, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange),
                                                   transform.position.y - 4.5f,
                                                   transform.position.z),
                                                   Quaternion.identity);

                    //音
                    SoundMgr.PlaySe("Spawn", 5);
                }
                break;


            //時間経過
            case EnemyState.Active:
                //攻撃
                interval += Time.deltaTime;
                if (interval >= 2)
                {
                    interval = 0.0f;

                    //弾
                    Instantiate(Shot, new Vector3(transform.position.x - 5,
                                                  transform.position.y - 4.5f,
                                                  transform.position.z - 3),
                                                  Quaternion.identity);

                    //弾
                    Instantiate(Shot, new Vector3(transform.position.x + 5,
                                                  transform.position.y - 4.5f,
                                                  transform.position.z - 3),
                                                  Quaternion.identity);

                    //音
                    SoundMgr.PlaySe("Shot", 1);
                }
                break;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        //弾がぶつかったら
        if (collision.gameObject.tag == "Shot")
        {
            HP -= 1;

            if (HP <= 0)
            {
                isDead = true;

                //スコア
                Score.score += getScore;

                //死亡エフェクト
                Instantiate(EffectDead, new Vector3(transform.position.x,
                                               transform.position.y,
                                               transform.position.z),
                                               Quaternion.identity);

                //音
                SoundMgr.PlaySe("Death", 2);
                SoundMgr.PlaySe("ClearFanfare", 7);
            }

            //Sliderに値を設定
            _Slider.value = HP;
        }
    }


    //HPバー
    IEnumerator HPBer()
    {
        yield return new WaitForSeconds(4.0f);
        _Slider.transform.Translate(0, 500, 0);
    }
}
