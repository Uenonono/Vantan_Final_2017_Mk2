using UnityEngine;
using System.Collections;

//シューティング   エネミー
public class EnemyMove : MonoBehaviour
{
    public float enemyY = -0.01f;
    public GameObject shot_e;
    public GameObject piece;


    void Start()
    {

    }


    void Update()
    {
        transform.Translate(0, enemyY, 0);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            //当たったら、消える
            Destroy(this.gameObject);

            Instantiate(piece, new Vector3(
           transform.position.x,
           transform.position.y,
           transform.position.z),
           Quaternion.identity);
        }
    }
}
