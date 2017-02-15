using UnityEngine;
using System.Collections;

//シューティング エネミースポナー
public class STGSpawner : MonoBehaviour
{
    //敵スポナー
    public GameObject SpawneObject;    //敵
    public float spawnRange; //スポーン範囲
    public float spawnIntervalTime; //スポーン間隔

    //どのスポナーかチェック
    //public bool attackSpawner;   //シャアスポナー
    public bool bossSpawner;     //ボススポナー
    public bool isEffect;    //エフェクト

    public float intervalTime;  //スポナーインターバルタイム?
    float interval = 0;


    void Start()
    {
        //エネミースポーン
        if (!bossSpawner && !isEffect)
        {
            InvokeRepeating("EnemyCreate", 0, spawnIntervalTime);
            Destroy(this.gameObject, 55);
        }

        //エフェクトスポーン
        if (isEffect)
        {
            InvokeRepeating("ParticleCreate", 0, spawnIntervalTime);
        }
    }


    void Update()
    {
        ////シャアスポナー
        //if (attackSpawner)
        //{
        //    interval += Time.deltaTime;
        //    if (interval >= intervalTime)
        //    {
        //        Instantiate(SpawneObject, new Vector3(transform.position.x,
        //               transform.position.y,
        //               transform.position.z),
        //               Quaternion.identity);
        //    }
        //}


        //ボススポナー
        if (bossSpawner)
        {
            interval += Time.deltaTime;
            if (interval >= intervalTime)
            {
                Instantiate(SpawneObject, new Vector3(transform.position.x,
                       transform.position.y,
                       transform.position.z),
                       Quaternion.identity);

                Destroy(this.gameObject);
            }
        }
    }


    //エネミースポーン
    void EnemyCreate()
    {
        Instantiate(SpawneObject, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange),
                                       transform.position.y,
                                       transform.position.z),
                                       Quaternion.identity);
    }


    //エフェクトスポーン
    void ParticleCreate()
    {
        Instantiate(SpawneObject, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange),
                                       transform.position.y + Random.Range(-spawnRange, spawnRange),
                                       transform.position.z + Random.Range(-spawnRange, spawnRange)),
                                       Quaternion.identity);
    }
}
