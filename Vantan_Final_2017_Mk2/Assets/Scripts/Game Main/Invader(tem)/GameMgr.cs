using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public static class STGGameState
{
    static int state = 0;

    public static void SetState(int val)
    {
        state = val;
    }

    public static int GetState()
    {
        return state;
    }
}


//ゲーム管理
public class GameMgr : MonoBehaviour
{
    //float activeTime;
    //float active;


    private void Start()
    {
        //activeTime = 3;
        //active = 0;

        //STGGameState.SetState(0);

        //SoundMgr.SoundLoadBgm("Window_Rule", "Invader/Window_Rule");
        //SoundMgr.SoundLoadSe("Start", "Invader/Start");
        //SoundMgr.PlayBgm("Window_Rule");
    }


    void Update()
    {
        ////ゲームオーバーorタイムアップでリザルト画面へ
        //if (STGPlayer.isDead || GameTime.isTimeUp || STGBoss.isDead)
        //{
        //    active += Time.deltaTime;
        //    if (active >= activeTime)
        //    {
        //        active = 0;
        //        SoundMgr.StopBgm();
        //        STGGameState.SetState(3);
        //        MSMM.RankingTempData.TempScore = (uint)Score.score;
        //        SceneManager.LoadScene("InvaderResult");
        //    }
        //}
    }


    public void Disable()
    {
        //active = 0;
        //Score.score = 0;
        //ResultScore.SScore = 0;
        //ResultScore.MainScore = 0;
        //STGGameState.SetState(4);
        //STGBoss.isDead = false;
        //STGPlayer.isDead = false;
        //Rules.isStart = false;
    }
}
