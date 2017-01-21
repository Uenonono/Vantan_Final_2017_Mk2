using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//制限時間
public class GameTime : MonoBehaviour
{
    [SerializeField]
    public Text timeText;

    public float time;
    public static float limitTime;     //制限時間
    int minite;                 //制限時間（分）
    float second;	            //制限時間（秒）
    int oldSecond;              //前回Update時の秒数

    public static bool isTimeUp;


    void Start()
    {
        timeText = timeText.GetComponent<Text>();

        limitTime = time;

        minite = ((int)(limitTime)) / 60;
        limitTime = minite * 60 + second;
        oldSecond = 0;
    }


    void Update()
    {
        if (Time.timeScale > 0 && limitTime > 0.0f)
        {
            limitTime = minite * 60 + second;
            //-1/s
            limitTime -= Time.deltaTime;

            //再設定
            minite = ((int)(limitTime)) / 60;
            second = limitTime - minite * 60;


            //時間を表示する
            if ((int)(second) != oldSecond)
            {
                timeText.text = minite.ToString("00") + ":" + ((int)second).ToString("00");
            }
            oldSecond = ((int)(second));

            //時間が0になったら
            if (limitTime <= 0.0f)
            {
                Debug.Log("終了");
                isTimeUp = true;
            }
        }
    }
}
