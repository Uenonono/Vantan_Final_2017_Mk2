using UnityEngine;
using System.Collections;

public class STGResultEnemy : MonoBehaviour
{

    public float Speed;

    enum enemyMove
    {
        Right,      //右
        Up,         //上
        Left,       //左
        Down,       //下

    };

    enemyMove enemymove = enemyMove.Right;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (enemymove)
        {
            case enemyMove.Right:
                transform.Translate(Speed, 0, 0);
                if (transform.position.x >= 8)
                {
                    enemymove = enemyMove.Up;
                }

                break;

            case enemyMove.Up:
                transform.Translate(0, Speed, 0);
                if (transform.position.y >= 4)
                {
                    enemymove = enemyMove.Left;
                }
                break;

            case enemyMove.Left:
                transform.Translate(-Speed, 0, 0);
                if (transform.position.x <= -8)
                {
                    enemymove = enemyMove.Down;
                }
                break;

            case enemyMove.Down:
                transform.Translate(0, -Speed, 0);
                if (transform.position.y <= -4)
                {
                    enemymove = enemyMove.Right;
                }
                break;

        }
    }
}
