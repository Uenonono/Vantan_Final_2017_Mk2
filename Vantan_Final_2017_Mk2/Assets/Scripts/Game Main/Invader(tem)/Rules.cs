using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//ルール画面テキスト
public class Rules : MonoBehaviour
{
    float interval;

    //ルール1
    [SerializeField]
    public Text text0;
    //ルール1
    [SerializeField]
    public Text text1;
    //ルール2
    [SerializeField]
    public Text text2;
    //ルール2
    [SerializeField]
    public Text text3;


    void Start()
    {

    }


    void Update()
    {
        interval += Time.deltaTime;
        if(interval >= 0)
        {
            text0.gameObject.SetActive(true);
        }
        else
        {
            text0.gameObject.SetActive(false);
        }
        if (interval >= 10)
        {
            text0.gameObject.SetActive(false);
            text1.gameObject.SetActive(true);
        }
        else
        {
            text1.gameObject.SetActive(false);
        }
        if (interval >= 20)
        {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(true);
        }
        else
        {
            text2.gameObject.SetActive(false);
        }
        if (interval >= 25)
        {
            text2.gameObject.SetActive(false);
            text3.gameObject.SetActive(true);
        }
        else
        {
            text3.gameObject.SetActive(false);
        }
    }
}
