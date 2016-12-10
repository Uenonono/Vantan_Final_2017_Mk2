using UnityEngine;
using System.Collections;

//シューティング エネミースポナー
public class STGSpawner : MonoBehaviour
{
    public float spawnRange; //スポーン範囲
    public float spawnIntervalTime; //スポーン間隔
    public GameObject Enemy;    //敵


    void Start()
    {
        InvokeRepeating("Create", 0, spawnIntervalTime);
    }


    void Update()
    {

    }


    //スポーン
    void Create()
    {
        Instantiate(Enemy, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange),
                                       transform.position.y,
                                       transform.position.z),
                                       Quaternion.identity);
    }
}
