using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//スコア
public class Score : MonoBehaviour
{
    public static int MasterScore;    //メインのスコア
    public static int score;    //加算をさせるスコア
    public  int MScore = 0;   //ゲーム終了時のスコア


    void Start()
    {
        MasterScore = 0;
        
    }


    void Update()
    {
        MasterScore = score;

        if(MasterScore < MScore)
        {
            MasterScore += (STGEnemy.setScore / 100);
        }
        

        //スコア表示
        GetComponent<Text>().text = "score:" + MasterScore.ToString();

        //if(GameTime.limitTime <= 0.0f)
        //{
        //    MScore = MasterScore;
        //}
    }
}
