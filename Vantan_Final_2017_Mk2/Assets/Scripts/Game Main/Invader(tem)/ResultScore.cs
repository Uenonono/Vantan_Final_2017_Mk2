using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ResultScore : MonoBehaviour
{
    private static int SScore;
    int MainScore;
    int MemoryScore;
     

    void Start()
    {
        MainScore = Score.score;
    }


    void Update()
    {
        //MemoryScore += MainScore;

        if (SScore < MainScore)
        {
            SScore += 10;
        }

        //スコア表示
        GetComponent<Text>().text = SScore.ToString();
    }
}
