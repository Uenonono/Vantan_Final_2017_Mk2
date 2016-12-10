using UnityEngine;
using System.Collections;

//シューティング エネミー
public class STGEnemy : MonoBehaviour
{
    public float speed; //移動速度
    float interval;
    public bool isAttack;   //攻撃エネミーか?
    public float intervalTime;  //発射間隔
    public GameObject Shot; //弾
    public int getScore;
    public GameObject Piece;    //死亡エフェクト

    void Start()
    {
        interval = 0;
    }


    void Update()
    {
        Move();
    }


    //行動
    void Move()
    {
        //移動
        transform.Translate(0, 0, speed);

        //攻撃
        if (isAttack)
        {
            interval += Time.deltaTime;
            if (interval >= intervalTime)
            {
                interval = 0.0f;

                Instantiate(Shot, new Vector3(transform.position.x,
                                              transform.position.y,
                                              transform.position.z - 1),
                                              Quaternion.identity);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            //当たったら消える
            Destroy(this.gameObject);

            Score.score += getScore;

            // Instantiate(piece, new Vector3(
            //transform.position.x,
            //transform.position.y,
            //transform.position.z),
            //Quaternion.identity);
        }


        if (collision.gameObject.tag == "Dead")
        {
            //当たったら消える
            Destroy(this.gameObject);
        }
    }
}
