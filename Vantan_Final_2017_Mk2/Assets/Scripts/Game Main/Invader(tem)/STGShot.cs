using UnityEngine;
using System.Collections;

//シューティング 弾
public class STGShot : MonoBehaviour
{
    private Renderer rend;

    public float speed; //速度
    public bool PlayerShot;  //弾の種類
    public bool isleft; //右

    public GameObject EffectDead;   //消滅エフェクト
    bool timeup;


    void Start()
    {
        rend = GetComponent<Renderer>();

        StartCoroutine(DeadEffectCreate());
        StartCoroutine(ColorChange());


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


        //サウンドロード
        SoundMgr.SoundLoadSe("Bounce", "Invader/Bounce");
    }


    void Update()
    {
        //ボスが死んだorタイムアップなら消滅
        if (STGBoss.isDead || GameTime.isTimeUp)
        {
            Instantiate(EffectDead, new Vector3(transform.position.x,
                    transform.position.y,
                    transform.position.z),
                    Quaternion.identity);

            Destroy(this.gameObject);
        }
    }


    //壁ぶつかったら音
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            SoundMgr.PlaySe("Bounce", 4);
        }
    }


    //消滅時エフェクト表示
    IEnumerator DeadEffectCreate()
    {
        yield return new WaitForSeconds(9.9f);
        Instantiate(EffectDead, new Vector3(transform.position.x,
                            transform.position.y,
                            transform.position.z),
                            Quaternion.identity);
    }


    //時間経過で点滅
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
