using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//ゲーム管理
public class GameMgr : MonoBehaviour
{
    //シーンの状態
    enum GameState
    {
        Title,  //タイトル
        Rule,   //ルール
        Main,   //本編
        Result, //リザルト
    }
    GameState gameState = GameState.Title;


    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //SoundMgr.SoundLoadBgm("img_Title2", "img_Title2");
    }


    void Update()
    {
        //シーン変更
        switch (gameState)
        {
            //タイトル
            case GameState.Title:
                //ボタン押したらルール画面へ
                if (Input.GetMouseButtonDown(0) && gameState == GameState.Title)
                {
                    gameState = GameState.Rule;
                    SceneManager.LoadScene("Rule");
                }
                break;

            //ルール
            case GameState.Rule:
                //ボタン押したらゲーム本編へ
                if (Input.GetMouseButtonDown(0) && gameState == GameState.Rule)
                {
                    gameState = GameState.Main;
                    SceneManager.LoadScene("Main");
                    //SoundMgr.PlayBgm("img_Title2");
                }
                break;

            //ゲーム本編
            case GameState.Main:
                //ゲームオーバーorタイムアップでリザルト画面へ
                if(gameState == GameState.Main && STGPlayer.isDead == true || GameTime.isTimeUp)
                {
                        gameState = GameState.Result;
                        SceneManager.LoadScene("Result");
                }
                break;

            //リザルト
            case GameState.Result:
                //ボタン押したらタイトル画面へ
                if (Input.GetMouseButtonDown(0) && gameState == GameState.Result)
                {
                    gameState = GameState.Title;
                    SceneManager.LoadScene("Title");

                    Score.MasterScore = 0;

                }
                break;
        }
    }
}
