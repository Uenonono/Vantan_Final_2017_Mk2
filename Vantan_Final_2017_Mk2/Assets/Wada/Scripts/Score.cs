using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//スコア
public class Score : MonoBehaviour
{
    int MasterScore;    //メインのスコア
    public static int score;    //加算をさせるスコア


    void Start()
    {

    }


    void Update()
    {
        MasterScore = score;

        //スコア表示
        GetComponent<Text>().text = "score:" + MasterScore.ToString();
    }
}
