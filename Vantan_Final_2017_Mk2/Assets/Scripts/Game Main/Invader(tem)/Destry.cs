using UnityEngine;
using System.Collections;

public class Destry : MonoBehaviour
{
    public float time;

    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
