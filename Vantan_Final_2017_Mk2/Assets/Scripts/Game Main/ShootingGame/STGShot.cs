using UnityEngine;
using System.Collections;

//シューティング 弾
public class STGShot : MonoBehaviour
{
    public float speed; //速度
    public bool enemyShot;


    void Start()
    {
        if (enemyShot)
        {
            this.GetComponent<Rigidbody>().AddForce(
            (transform.forward + transform.right) * speed, ForceMode.VelocityChange);
        }
        else
        {
            this.GetComponent<Rigidbody>().AddForce(
            (transform.forward) * speed, ForceMode.VelocityChange);
        }

        //カオスになるのでとりあえず消滅させる
        Destroy(this.gameObject, 10);
    }


    void Update()
    {

    }
}
