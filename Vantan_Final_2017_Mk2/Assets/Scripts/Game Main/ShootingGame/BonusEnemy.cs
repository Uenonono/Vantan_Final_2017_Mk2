using UnityEngine;
using System.Collections;

//シューティング ボーナスエネミー
public class BonusEnemy : MonoBehaviour
{
    public float speed; //移動速度
    public int getScore;
    public GameObject Piece;    //死亡エフェクト


    void Start()
    {

    }


    void Update()
    {
        //移動
        transform.Translate(speed, 0, 0);
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
    }
}
