using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    int s = 0; //数の制限
    GameObject enemy_generation;

    public static bool enemyFlag = false;


    void Start()
    {
        //敵の生成
        enemy_generation = (GameObject)Resources.Load("enemy");
       

        if (s < 5)
        {
            InvokeRepeating("Create", 0, 1.0f);

            enemyFlag = true;
        }
    }


    void Update()
    {


    }


    void Create()
    {
            Instantiate(enemy_generation, new Vector3(
           transform.position.x + Random.Range(-10, 10),
           transform.position.y ,
           transform.position.z),
           Quaternion.identity);
    }
}
