using UnityEngine;
using System.Collections;

public class STGTitleEffect : MonoBehaviour
{
    public GameObject TEffect;
    float Speed; //速度
    float TTime;

    // Use this for initialization
    void Start()
    {
        Speed =0.5f;
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed);
    }
}
