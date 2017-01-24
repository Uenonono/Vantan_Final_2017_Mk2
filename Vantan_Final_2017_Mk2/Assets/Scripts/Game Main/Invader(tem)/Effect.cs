using UnityEngine;
using System.Collections;

//エフェクト
public class Effect : MonoBehaviour
{
    public float time;

    void Start()
    {
        Destroy(this.gameObject, time);
    }


    void Update()
    {

    }
}
