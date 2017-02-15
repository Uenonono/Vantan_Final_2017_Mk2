using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//シーンの状態
public enum GameState
{
    Title,  //タイトル
    Rule,   //ルール
    Main,   //本編
    Result, //リザルト
    Disabled, //なし
}


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
    float activeTime;
    float active;


    private void Start()
    {
        activeTime = 3;
        active = 0;

        STGGameState.SetState(0);

        SoundMgr.SoundLoadSe("Start", "Invader/Start");
        SoundMgr.SoundLoadBgm("Window_Rule", "Invader/Window_Rule");
        SoundMgr.PlayBgm("Window_Rule");
    }


    void Awake()
    {
        if (FindObjectsOfType<GameMgr>().Length != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void Update()
    {
        //シーン変更
        switch ((GameState)STGGameState.GetState())
        {
            //タイトル
            case GameState.Title:
                //ボタン押したらルール画面へ
                if (Input.GetAxis("BottomRed") == 1 || Input.GetAxis("BottomGreen") == 1 || Input.GetAxis("BottomBlue") == 1 || Input.GetAxis("BottomYellow") == 1)
                {
                    SoundMgr.PlaySe("Start", 6);
                    STGGameState.SetState(1);
                    SceneManager.LoadScene("InvaderRule");
                }
                break;


            //ルール
            case GameState.Rule:
                //ボタン押したらゲーム本編へ
                if (Rules.isStart)
                {
                    SoundMgr.PlaySe("Start", 6);
                    STGGameState.SetState(2);
                    SceneManager.LoadScene("InvaderMain");
                    SoundMgr.StopBgm();
                }
                break;


            //ゲーム本編
            case GameState.Main:
                //ゲームオーバーorタイムアップでリザルト画面へ
                if (STGPlayer.isDead || GameTime.isTimeUp || STGBoss.isDead)
                {
                    active += Time.deltaTime;
                    if (active >= activeTime)
                    {
                        active = 0;
                        SoundMgr.StopBgm();
                        STGGameState.SetState(3);
                        MSMM.RankingTempData.TempScore = (uint)Score.score;
                        SceneManager.LoadScene("InvaderResult");
                    }
                }
                break;


            //リザルト
            case GameState.Result:
                if (Input.GetAxis("BottomRed") == 1)
                {
                    Disable();
                    STGGameState.SetState(0);
                    SceneManager.LoadScene("InvaderTitle");
                }
                if (Input.GetAxis("BottomGreen") == 1)
                {
                    Disable();
                    SoundMgr.StopBgm();
                    SceneManager.LoadScene("MainTitle");
                }
                break;
        }
    }


    public void Disable()
    {
        active = 0;
        Score.score = 0;
        ResultScore.SScore = 0;
        ResultScore.MainScore = 0;
        STGGameState.SetState(4);
        STGBoss.isDead = false;
        STGPlayer.isDead = false;
        Rules.isStart = false;
    }
}
