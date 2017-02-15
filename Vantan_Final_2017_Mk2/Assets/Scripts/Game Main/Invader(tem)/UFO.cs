using UnityEngine;
using System.Collections;

//UFO
public class UFO : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
