using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//スコア
public class Score : MonoBehaviour
{
    public static int MasterScore;    //メインのスコア
    public static int score;    //加算をさせるスコア
    public static int MScore;   //ゲーム終了時のスコア


    void Start()
    {
        MasterScore = 0;
    }


    void Update()
    {
        MasterScore = score;

        //スコア表示
        GetComponent<Text>().text = "score:" + MasterScore.ToString();

        if(GameTime.limitTime <= 0.0f)
        {
            MScore = MasterScore;
        }
    }
}
