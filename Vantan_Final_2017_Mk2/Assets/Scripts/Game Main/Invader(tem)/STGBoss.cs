using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//ボス
public class STGBoss : MonoBehaviour
{
    public int hp;  //hp
    int HP;
    public int getScore;    //スコア
    public GameObject Piece;    //破片
    public GameObject Effect;   //死亡エフェクト

    public GameObject Shot; //弾
    public float intervalTime;  //発射間隔
    float interval;

    public GameObject Enemy;    //敵
    public float spawnRange; //スポーン範囲

    Slider _Slider;
    public static bool isDead;  //生死フラグ


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
    bool isNormal;


    void Start()
    {
        //サウンドロードSpawn
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
        SoundMgr.SoundLoadSe("Spawn", "Invader/Spawn");

        HP = hp;
        _Slider = GameObject.Find("BossHPSlider").GetComponent<Slider>();
        //Sliderに値を設定
        _Slider.maxValue = HP;
        _Slider.value = HP;
        isDead = false;

        StartCoroutine(Count());
    }


    void Update()
    {
        Move();
    }


    public void Move()
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
                //敵スポーン
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

                //ステータス変更
                active += Time.deltaTime;
                if (active >= activeTime)
                {
                    enemyState = EnemyState.Active;
                }

                break;

            //時間経過
            case EnemyState.Active:

                //攻撃
                interval += Time.deltaTime;
                if (interval >= intervalTime)
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
        //弾
        if (collision.gameObject.tag == "Shot")
        {
            HP -= 1;

            if (HP <= 0)
            {
                isDead = true;

                //当たったら消える
                Destroy(this.gameObject);

                //スコア
                Score.score += getScore;

                //エフェクトピース
                Instantiate(Piece, new Vector3(transform.position.x,
                                               transform.position.y,
                                               transform.position.z),
                                               Quaternion.identity);

                //エフェクト
                Instantiate(Effect, new Vector3(transform.position.x - 3,
                                               transform.position.y,
                                               transform.position.z),
                                               Quaternion.identity);

                //音
                SoundMgr.PlaySe("Death", 2);
            }

            //Sliderに値を設定
            _Slider.value = HP;
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(4.0f);
        _Slider.transform.Translate(0, 500, 0);
    }
}
