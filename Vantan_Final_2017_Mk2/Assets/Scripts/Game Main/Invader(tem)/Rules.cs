using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//ルール画面
public class Rules : MonoBehaviour
{
    float speed = -0.2f;
    float interval;

    //ルール1
    [SerializeField]
    public Text text1;
    //ルール2
    [SerializeField]
    public Text text2;
    //ルール2
    [SerializeField]
    public Text text3;

    bool isS;
    float interval2;

    public static bool isStart;


    void Start()
    {
        isS = false;
        isStart = false;

        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
    }


    void Update()
    {
        Test();
    }


    void Test()
    {
        interval += Time.deltaTime;
        if (interval <= 20)
        {
            interval = 0;
            transform.Translate(0, speed, 0);
        }
        if (interval >= 21 && Input.GetAxis("BottomRed") == 1 || Input.GetAxis("BottomGreen") == 1 || Input.GetAxis("BottomBlue") == 1 || Input.GetAxis("BottomYellow") == 1)
        {
            isS = true;
        }
        if (isS)
        {
            interval2 += Time.deltaTime;
            transform.Translate(0, 0, 3);
        }

        if (interval2 >= 1)
        {
            isStart = true;
        }
    }
}
