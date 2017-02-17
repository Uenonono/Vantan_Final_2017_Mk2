using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//リザルトスコア
public class ResultScore : MonoBehaviour
{
    public static int SScore;
    public static int MainScore;
    int MemoryScore;

    //スコア表示
    [SerializeField]
    public Text score1;

    [SerializeField]
    public Text score2;
    
    
    void Start()
    {
        MainScore = 0;
        MainScore = Score.score;

        //BGM
        //SoundMgr.SoundLoadBgm("Result_A", "Invader/Result_A");
        //SoundMgr.PlayBgm("Result_A");

        score1 = score1.GetComponent<Text>();
        score2 = score2.GetComponent<Text>();
        MSMM.RankingTempData.TempScore = (uint)MainScore;


    }


    void Update()
    {

        if (SScore < MainScore)
        {
            SScore += 100;
        }
        score1.text = "スコア";
        score2.text = SScore.ToString();
      
    }
}
