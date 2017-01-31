using UnityEngine;
using System.Collections;

public class STGBoss : MonoBehaviour
{
    public int hp;  //hp
    public int getScore;    //スコア
    public GameObject Piece;    //死亡エフェクト
    public GameObject Effect0;    //死亡エフェクト
    public GameObject Effect1;    //死亡エフェクト
    public GameObject Effect2;    //死亡エフェクト

    public GameObject Shot; //弾
    public float intervalTime;  //発射間隔
    float interval;

    public float spawnRange0; //スポーン範囲
    public float spawnIntervalTime0; //スポーン間隔
    public GameObject Enemy0;    //敵

    public float spawnRange1; //スポーン範囲
    public float spawnIntervalTime1; //スポーン間隔
    public GameObject Enemy1;    //敵

    public float spawnRange2; //スポーン範囲
    public float spawnIntervalTime2; //スポーン間隔
    public GameObject Enemy2;    //敵


    void Start()
    {
        //サウンドロードSpawn
        SoundMgr.SoundLoadSe("Shot", "Invader/Shot");
        SoundMgr.SoundLoadSe("Death", "Invader/Death");
        SoundMgr.SoundLoadSe("Spawn", "Invader/Spawn");

        InvokeRepeating("Create0", 0, spawnIntervalTime0);
        InvokeRepeating("Create1", 0, spawnIntervalTime1);
        InvokeRepeating("Create2", 0, spawnIntervalTime2);
    }


    void Update()
    {
        //攻撃
        interval += Time.deltaTime;
        if (interval >= intervalTime)
        {
            interval = 0.0f;

            //弾
            Instantiate(Shot, new Vector3(transform.position.x,
                                          transform.position.y - 5,
                                          transform.position.z - 1),
                                          Quaternion.identity);

            //音
            SoundMgr.PlaySe("Shot", 3);
        }
    }


    //敵スポーン
    void Create0()
    {
        Instantiate(Enemy0, new Vector3(transform.position.x + Random.Range(-spawnRange0, spawnRange0),
                                       transform.position.y - 5,
                                       transform.position.z),
                                       Quaternion.identity);

        //音
        SoundMgr.PlaySe("Spawn", 5);
    }

    void Create1()
    {
        Instantiate(Enemy1, new Vector3(transform.position.x + Random.Range(-spawnRange1, spawnRange1),
                                       transform.position.y - 5,
                                       transform.position.z),
                                       Quaternion.identity);

        //音
        SoundMgr.PlaySe("Spawn", 5);
    }

    void Create02()
    {
        Instantiate(Enemy2, new Vector3(transform.position.x + Random.Range(-spawnRange2, spawnRange2),
                                       transform.position.y - 5,
                                       transform.position.z),
                                       Quaternion.identity);

        //音
        SoundMgr.PlaySe("Spawn", 5);
    }


    void OnCollisionEnter(Collision collision)
    {
        //弾
        if (collision.gameObject.tag == "Shot")
        {
            hp -= 1;

            if(hp <= 0)
            {
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
                Instantiate(Effect0, new Vector3(transform.position.x - 3,
                                               transform.position.y,
                                               transform.position.z),
                                               Quaternion.identity);
                //エフェクト
                Instantiate(Effect1, new Vector3(transform.position.x,
                                               transform.position.y,
                                               transform.position.z),
                                               Quaternion.identity);
                //エフェクト
                Instantiate(Effect2, new Vector3(transform.position.x + 3,
                                               transform.position.y,
                                               transform.position.z),
                                               Quaternion.identity);

                //音
                SoundMgr.PlaySe("Death", 4);
            }
        }

        //壁
        if (collision.gameObject.tag == "Dead")
        {
            //当たったら消える
            Destroy(this.gameObject);
        }
    }
}
