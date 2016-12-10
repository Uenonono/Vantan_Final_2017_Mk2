using UnityEngine;
using System.Collections;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    float timer;
    public float interval = 3;
    int count = 1;


    void Start()
    {

    }


    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            Spawn();
            timer = 0;
        }
    }


    void Spawn()
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
