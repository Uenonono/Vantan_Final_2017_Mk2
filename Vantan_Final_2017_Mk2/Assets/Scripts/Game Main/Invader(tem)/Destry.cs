using UnityEngine;
using System.Collections;

//消す
public class Destry : MonoBehaviour
{
    public float time;

    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
