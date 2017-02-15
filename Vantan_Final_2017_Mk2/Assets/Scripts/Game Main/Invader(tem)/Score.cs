using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//スコア
public class Score : MonoBehaviour
{
    public static int MasterScore;    //メインのスコア
    public static int score;    //加算をさせるスコア

    public int mScore;

    void Start()
    {
        MasterScore = 0;
        score = 0;
    }

    void Update()
    {
        MasterScore = score;

        //スコア表示
        GetComponent<Text>().text = "" + MasterScore.ToString();
    }
}
