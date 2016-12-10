using UnityEngine;
using System.Collections;

//シューティング プレイヤー
public class STGPlayer : MonoBehaviour
{
    public float speed; //移動速度
    float interval;
    public float intervalTime;  //発射間隔
    public GameObject Shot; //弾


    void Start()
    {
        interval = 0;
    }


    void Update()
    {
        Move();
    }


    //プレイヤーの行動
    void Move()
    {
        //移動
        if (Input.GetKey("a"))
        {
            transform.Translate(-speed, 0, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(speed, 0, 0);
        }


        //ショット
        interval += Time.deltaTime;
        if (Input.GetKey("space"))
        {
            if (interval >= intervalTime)
            {
                interval = 0.0f;

                Instantiate(Shot, new Vector3(transform.position.x,
                                              transform.position.y,
                                              transform.position.z + 1.5f),
                                              Quaternion.identity);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shot" || collision.gameObject.tag == "EnemyShot")
        {
            //当たったら消える
            Destroy(this.gameObject);
        }
    }
}
