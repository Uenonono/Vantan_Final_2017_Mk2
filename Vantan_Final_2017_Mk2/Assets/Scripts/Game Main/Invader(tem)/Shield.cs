using UnityEngine;
using System.Collections;

//シ－ルド
public class Shield : MonoBehaviour
{
    public GameObject EffectShield;    //シ－ルドエフェクト


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            //シ－ルドエフェクト
            Instantiate(EffectShield, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);
        }
    }
}
