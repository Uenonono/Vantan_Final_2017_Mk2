using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//制限時間
public class GameTime : MonoBehaviour
{
    //制限時間テキスト
    [SerializeField]
    public Text timeText;

    public float time;
    public static float limitTime;     //制限時間
    int minite;                 //制限時間（分）
    float second;	            //制限時間（秒）
    int oldSecond;              //前回Update時の秒数

    bool isBoss;
    public static bool isTimeUp;

    //ゲームスタート
    [SerializeField]
    public Text GameStartText;
    [SerializeField]
    public Text GameStartText2;
    float activeTime;
    float active;
    public static bool isCount = false;

    //ゲームオーバー
    [SerializeField]
    public Text GameOvertext;
    //タイムアップ
    [SerializeField]
    public Text TimeUptext;
    //ゲームクリア
    [SerializeField]
    public Text GameCleartext;

    //ワーニング
    public GameObject wariningbanner;
    public GameObject warinng;


    void Start()
    {
        active = 0;
        activeTime = 4;
        timeText = timeText.GetComponent<Text>();
        isCount = false;
        isTimeUp = false;

        limitTime = time;

        minite = ((int)(limitTime)) / 60;
        limitTime = minite * 60 + second;
        oldSecond = 0;


        GameStartText = GameStartText.GetComponent<Text>();
        GameStartText2 = GameStartText2.GetComponent<Text>();
        StartCoroutine(Count());

        SoundMgr.SoundLoadBgm("img_Title2", "Invader/img_Title2");
        SoundMgr.SoundLoadBgm("Boss00", "Invader/Boss00");
        SoundMgr.SoundLoadSe("Count_3,2,1", "Invader/Count_3,2,1");
        SoundMgr.SoundLoadSe("BossAlert", "Invader/BossAlert");
        SoundMgr.SoundLoadSe("TimeOver_A", "Invader/TimeOver_A");
    }


    void Update()
    {
        active += Time.deltaTime;
        if (active >= activeTime)
        {
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


            //ワーニング
            if (limitTime <= 65)
            {
                isBoss = true;
            }
            if (limitTime <= 62)
            {
                isBoss = false;
            }

            //時間が0になったら
            if (limitTime <= 0.0f)
            {
                isTimeUp = true;
            }
        }

        //ワーニングテキスト
        if (isBoss)
        {
            wariningbanner.gameObject.SetActive(true);
            warinng.gameObject.SetActive(true);
        }
        else
        {
            wariningbanner.gameObject.SetActive(false);
            warinng.gameObject.SetActive(false);
        }

        //ゲーム終了テキスト
        if (!STGPlayer.isDead && isTimeUp && !STGBoss.isDead)
        {
            TimeUptext.gameObject.SetActive(true);
            SoundMgr.StopBgm();
        }
        else
        {
            TimeUptext.gameObject.SetActive(false);
        }


        //ゲームオーバーテキスト
        if (STGPlayer.isDead)
        {
            GameOvertext.gameObject.SetActive(true);
            SoundMgr.StopBgm();
        }
        else
        {
            GameOvertext.gameObject.SetActive(false);
        }


        //ゲームクリアテキスト
        if (!STGPlayer.isDead && STGBoss.isDead && !isTimeUp)
        {
            GameCleartext.gameObject.SetActive(true);
            SoundMgr.StopBgm();
        }
        else
        {
            GameCleartext.gameObject.SetActive(false);
        }
    }


    //スタートのカウント
    IEnumerator Count()
    {
        GameStartText.text = "Standby";
        GameStartText2.text = "Standby";
        yield return new WaitForSeconds(1.0f);

        GameStartText.text = "3";
        GameStartText2.text = "3";
        SoundMgr.PlaySe("Count_3,2,1", 0);
        yield return new WaitForSeconds(1.0f);

        GameStartText.text = "2";
        GameStartText2.text = "2";
        SoundMgr.PlaySe("Count_3,2,1", 0);
        yield return new WaitForSeconds(1.0f);

        GameStartText.text = "1";
        GameStartText2.text = "1";
        SoundMgr.PlaySe("Count_3,2,1", 0);
        yield return new WaitForSeconds(1.0f);

        GameStartText.text = "GAME START";
        GameStartText2.text = "GAME START";
        SoundMgr.PlayBgm("img_Title2");
        yield return new WaitForSeconds(1.0f);

        GameStartText.gameObject.SetActive(false);

        yield return new WaitForSeconds(48.0f);
        SoundMgr.StopBgm();

        yield return new WaitForSeconds(6.0f);
        SoundMgr.PlaySe("BossAlert", 6);
        yield return new WaitForSeconds(1.25f);
        SoundMgr.PlaySe("BossAlert", 6);
        yield return new WaitForSeconds(1.25f);
        SoundMgr.PlaySe("BossAlert", 6);
        yield return new WaitForSeconds(1.25f);

        yield return new WaitForSeconds(0.0f);
        SoundMgr.PlayBgm("Boss00");

        yield return new WaitForSeconds(60.0f);
        SoundMgr.PlaySe("TimeOver_A", 8);
    }
}
