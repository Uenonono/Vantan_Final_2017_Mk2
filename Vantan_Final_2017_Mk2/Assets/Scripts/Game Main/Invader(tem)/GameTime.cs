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

    [SerializeField]
    public Text starttimeText;
    float activeTime;
    float active;
    public static bool isCount = false;

    [SerializeField]
    public Text GameOvertext;
    [SerializeField]
    public Text TimeUptext;
    [SerializeField]
    public Text GameCleartext;


    void Start()
    {
        active = 0;
        activeTime = 3;
        timeText = timeText.GetComponent<Text>();
        isCount = false;
        isTimeUp = false;

        limitTime = time;

        minite = ((int)(limitTime)) / 60;
        limitTime = minite * 60 + second;
        oldSecond = 0;


        starttimeText = starttimeText.GetComponent<Text>();
        StartCoroutine(Count());


        GameOvertext = GameOvertext.GetComponent<Text>();
        TimeUptext = TimeUptext.GetComponent<Text>();
    }


    void Update()
    {
        active += Time.deltaTime;
        if (active >= activeTime)
        {
            //active = 0;
            isCount = true;
        }

        if (isCount && Time.timeScale > 0 && limitTime > 0.0f)
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
                isTimeUp = true;
            }
        }

        //ゲーム終了テキスト
        if (isTimeUp)
        {
            TimeUptext.gameObject.SetActive(true);
        }
        else
        {
            TimeUptext.gameObject.SetActive(false);
        }


        //ゲームオーバーテキスト
        if (STGPlayer.isDead)
        {
            GameOvertext.gameObject.SetActive(true);
        }
        else
        {
            GameOvertext.gameObject.SetActive(false);
        }


        //ゲームクリアテキスト
        if (STGBoss.isDead)
        {
            GameCleartext.gameObject.SetActive(true);
        }
        else
        {
            GameCleartext.gameObject.SetActive(false);
        }
    }


    //スタートのカウント
    IEnumerator Count()
    {
        starttimeText.text = "3";
        yield return new WaitForSeconds(1.0f);

        starttimeText.text = "2";
        yield return new WaitForSeconds(1.0f);

        starttimeText.text = "1";
        yield return new WaitForSeconds(1.0f);

        starttimeText.text = "GameStart";
        yield return new WaitForSeconds(1.0f);

        starttimeText.gameObject.SetActive(false);
    }
}
