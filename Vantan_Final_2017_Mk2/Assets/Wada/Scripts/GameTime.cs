using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameTime : MonoBehaviour
{
    public float limitTime;//　制限時間
    private int minite = 1;     //　制限時間（分）
    private float second = 0.0f;	//　制限時間（秒）
    private int oldSecond = 0;//　前回Update時の秒数
    private static bool timerFlag = false;//　タイマーフラグ
    [SerializeField]
    public Text timeText;


    void Awake()
    {
        limitTime = minite * 60 + second;

        timeText = timeText.GetComponent<Text>();
    }


    void Update()
    {
        //クリックしたら、カウントダウン開始
        if (Input.GetMouseButtonDown(0))
        {
            timerFlag = true;
        }
        //制限時間が開始
        if (timerFlag)
        {
            if (Time.timeScale > 0 && limitTime > 0.0f)
            {
                //一旦トータルの制限時間を計測
                limitTime = minite * 60 + second;
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
                }
            }
        }
    }
}
