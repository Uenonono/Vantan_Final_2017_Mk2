using UnityEngine;
using System.Collections;

//UFO回転
public class UFO : MonoBehaviour
{
    public float speed; //速度


    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
