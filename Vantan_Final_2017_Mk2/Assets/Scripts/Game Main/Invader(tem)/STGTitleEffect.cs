using UnityEngine;
using System.Collections;

public class STGTitleEffect : MonoBehaviour
{
    public GameObject Effect;

    float y = Random.Range(175.0f, 185.0f);
    // Use this for initialization
    void Start()
    {
       

      //  Instantiate(Effect, new Vector3(0, y, 0), Quaternion.identity);
         
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(Effect, new Vector3(0, y, 0), Quaternion.identity);

    }
}
