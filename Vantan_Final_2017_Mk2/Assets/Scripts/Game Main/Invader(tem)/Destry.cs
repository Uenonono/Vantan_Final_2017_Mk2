using UnityEngine;
using System.Collections;

//消す
public class Destry : MonoBehaviour
{
    public float time;  //消滅時間


    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
