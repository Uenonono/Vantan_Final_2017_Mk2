using UnityEngine;
using System.Collections;

//シューティング 弾
public class STGShot : MonoBehaviour
{
    public float speed; //速度
    public bool enemyShot;  //弾の種類


    void Start()
    {
        //サウンドロード
        SoundMgr.SoundLoadSe("Bounce", "Bounce");

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


    void OnCollisionEnter(Collision collision)
    {
        //壁ぶつかったら
        if (collision.gameObject.tag == "Wall")
        {
            //音
            SoundMgr.PlaySe("Bounce", 6);
        }
    }
}
