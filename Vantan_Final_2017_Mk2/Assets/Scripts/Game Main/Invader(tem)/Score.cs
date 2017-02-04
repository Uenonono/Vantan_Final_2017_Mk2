using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//スコア
public class Score : MonoBehaviour
{
    public static int MasterScore = 0;    //メインのスコア
    public static int score = 0;    //加算をさせるスコア
    public int SetScore = 0;

    public int mScore;

    void Start()
    {
    }


    void Update()
    {
        MasterScore = score;


        
        //スコア表示
        GetComponent<Text>().text = "score:" + MasterScore.ToString();

        //if(GameTime.limitTime <= 0.0f)
        //{
        //    MScore = MasterScore;
        //}
    }
}
