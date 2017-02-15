using UnityEngine;
using System.Collections;

//シューティング 弾
public class STGShot : MonoBehaviour
{
    public float speed; //速度
    public bool PlayerShot;  //弾の種類
    public bool isleft; //右

    private Renderer rend;


    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(ColorChange());

        //サウンドロード
        SoundMgr.SoundLoadSe("Bounce", "Invader/Bounce");

        //プレイヤー
        if (PlayerShot)
        {
            this.GetComponent<Rigidbody>().AddForce(
            (transform.forward) * speed, ForceMode.VelocityChange);
        }

        //敵
        if (!PlayerShot && isleft)
        {
            this.GetComponent<Rigidbody>().AddForce(
            (transform.forward + transform.right) * -speed, ForceMode.VelocityChange);
        }
        if (!PlayerShot && !isleft)
        {
            this.GetComponent<Rigidbody>().AddForce(
            (transform.forward - transform.right) * -speed, ForceMode.VelocityChange);
        }

        //10s消滅させる
        Destroy(this.gameObject, 10);
    }


    void Update()
    {
        if (STGBoss.isDead)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //壁ぶつかったら
        if (collision.gameObject.tag == "Wall")
        {
            //音
            SoundMgr.PlaySe("Bounce", 4);
        }
    }


    IEnumerator ColorChange()
    {
        yield return new WaitForSeconds(7f); //待ち時間
        var origenMaterial = new Material(rend.material);
        for (;;)
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", new Color(1, 1, 1));
            yield return new WaitForSeconds(0.1f); //待ち時間
            rend.material = origenMaterial; //元に戻す
            yield return new WaitForSeconds(0.1f); //繰り返し待ち時間

        }
    }
}
