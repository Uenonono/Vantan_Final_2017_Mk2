using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class STGTitle : MonoBehaviour
{
    [SerializeField]
    Vector3 MaxSize = Vector3.one;

    [SerializeField]
    Vector3 MinSize = Vector3.zero;

    [SerializeField]
    float AngularFrequency = 1.0f;

    private float Time;

    Text TitleName;



    // Use this for initialization
    void Start()
    {
      Time = 0.0f;

   
       
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Text>().text = "Button To Press";

        Time += AngularFrequency * UnityEngine.Time.deltaTime;
        transform.localScale = Vector3.Lerp(MinSize, MaxSize, 0.5f * Mathf.Sin(Time) + 0.5f);
    }
}
