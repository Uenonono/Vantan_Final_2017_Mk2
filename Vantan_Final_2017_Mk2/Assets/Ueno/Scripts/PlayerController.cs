using UnityEngine;
using System.Collections;

//プレイヤーの操作
public class PlayerController : MonoBehaviour
{
    public float speed;       //移動スピード
    public float jumpPower;  //ジャンプ力

    public bool isMove3D; //2D or 3D?
    public bool isVertical;     //上下移動する
    public bool isJump;         //ジャンプする
    bool isFloor;       //地面にいるかどうか


    void Start()
    {

    }


    void Update()
    {
        Move();

        if (isJump) Jump();
    }


    //移動
    void Move()
    {
        //3D
        if (isMove3D)
        {
            //左右移動
            float X = Input.GetAxis("Horizontal");
            float Z = Input.GetAxis("Vertical");
            Vector3 vec3 = GetComponent<Rigidbody>().velocity;

            if (X != 0)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(X * speed, vec3.y, vec3.z);
            }

            //上下移動(isVerticalがtrueなら操作可能)
            if (isVertical && Z != 0)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(vec3.x, vec3.y, Z * speed);
            }
        }
        //2D
        else
        {
            //左右移動
            float X = Input.GetAxis("Horizontal");
            float Y = Input.GetAxis("Vertical");
            Vector2 vec2 = GetComponent<Rigidbody2D>().velocity;

            if (X != 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(X * speed, vec2.y);
            }

            //上下移動(isVerticalがtrueなら操作可能)
            if (isVertical && Y != 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(vec2.x, Y * speed);
            }
        }
    }


    //ジャンプ
    void Jump()
    {
        //とりあえずスペースキー
        if (Input.GetKeyDown(KeyCode.Space) && !isFloor)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        }
    }

}
