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
        SoundMgr.SoundLoadBgm("img_Title2", "Invader/img_Title2");
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
                if (Input.GetAxis("BottomGreen") == 1)
                {
                    STGGameState.SetState(1);
                    SceneManager.LoadScene("InvaderRule");
                }
                break;

            //ルール
            case GameState.Rule:
                //ボタン押したらゲーム本編へ
                if (Input.GetAxis("BottomGreen") == 1)
                {
                    STGGameState.SetState(2);
                    SceneManager.LoadScene("InvaderMain");
                    SoundMgr.PlayBgm("img_Title2");
                }
                break;

            //ゲーム本編
            case GameState.Main:
                //ゲームオーバーorタイムアップでリザルト画面へ
                if (STGPlayer.isDead || GameTime.isTimeUp)
                {
                    active += Time.deltaTime;
                    if (active >= activeTime)
                    {
                        active = 0;
                        SoundMgr.StopBgm();
                        STGGameState.SetState(3);
                        SceneManager.LoadScene("InvaderResult");

                    }
                }
                break;

            //リザルト
            case GameState.Result:
                //ボタン押したらタイトル画面へ
                if (Input.GetAxis("BottomGreen") == 1)
                {
                    Score.MasterScore = 0;

                    STGGameState.SetState(0);
                    SceneManager.LoadScene("InvaderTitle");
                }

                //ボタン押したらタイトル画面へ
                if (Input.GetAxis("BottomRed") == 1)
                {
                    Disable();
                    SceneManager.LoadScene("MainTitle");
                }
                break;
        }
    }


    public void Disable()
    {
        active = 0;
        Score.MasterScore = 0;
        STGGameState.SetState(4);
    }
}
