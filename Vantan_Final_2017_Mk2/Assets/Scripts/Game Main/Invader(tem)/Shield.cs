using UnityEngine;
using System.Collections;

//シ－ルド
public class Shield : MonoBehaviour
{
    public GameObject Effect;    //エフェクト


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            //エフェクト
            Instantiate(Effect, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);
        }
    }
}
