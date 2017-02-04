using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class ResultScore : MonoBehaviour
{
    private static int SetScore = 0;
    private int MemoryScore = 0;
    private int MainScore = 0;
     
    // Use this for initialization
    void Start()
    {
        SetScore = Score.score;


    }

    // Update is called once per frame
    void Update()
    {


        MemoryScore += SetScore;

        if (MainScore < MemoryScore)
        {
            MainScore += (SetScore / 10);
        }


        //スコア表示
        GetComponent<Text>().text = MainScore.ToString();
    }
}
