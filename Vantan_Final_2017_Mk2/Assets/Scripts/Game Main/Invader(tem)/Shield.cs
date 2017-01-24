using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public GameObject Effect;    //エフェクト


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            //エフェクト
            Instantiate(Effect, new Vector3(transform.position.x,
                                           transform.position.y,
                                           transform.position.z),
                                           Quaternion.identity);

            //音
            //SoundMgr.PlaySe("Death", 4);
        }
    }
}
