using UnityEngine;
using System.Collections;

//シューティング エネミースポナー
public class STGSpawner : MonoBehaviour
{
    public float spawnRange; //スポーン範囲
    public float spawnIntervalTime; //スポーン間隔
    public GameObject Enemy;    //敵

    public bool bosspawner;
    public float intervalTime = 60;
    float interval = 0;

    void Start()
    {
        if(!bosspawner)
        {
            InvokeRepeating("Create", 0, spawnIntervalTime);
            SoundMgr.SoundLoadSe("Spawn", "Invader/Spawn");

            Destroy(this.gameObject, 55);
        }
    }


    void Update()
    {
        if (bosspawner)
        {
            interval += Time.deltaTime;
            if (interval >= intervalTime)
            {
                Instantiate(Enemy, new Vector3(transform.position.x,
                       transform.position.y,
                       transform.position.z),
                       Quaternion.identity);

                Destroy(this.gameObject);
            }
        }
    }


    //スポーン
    void Create()
    {
        Instantiate(Enemy, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange),
                                       transform.position.y,
                                       transform.position.z),
                                       Quaternion.identity);

        //音
        SoundMgr.PlaySe("Spawn", 5);
    }
}
