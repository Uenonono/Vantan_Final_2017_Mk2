using UnityEngine;
using System.Collections;

public class STGTitleEffect : MonoBehaviour
{
    public GameObject TEffect;
    float Speed; //速度
    float TTime;
    float Change;

    // Use this for initialization
    void Start()
    {
        Change = 0;
        Speed =0.5f;
       
    }

    // Update is called once per frame
    void Update()
    {

        TTime += Time.deltaTime;

        if(TTime > 1.0f)
        {
            Destroy(this.gameObject);
        }

        Instantiate(TEffect, new Vector3(transform.position.x + 1.0f, 0, transform.position.z + 1.5f),Quaternion.identity);

        transform.Translate(0, 0, Speed);

       
    }
}
